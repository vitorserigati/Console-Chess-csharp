namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
using ConsoleChessLibrary.Table.Exception;
public class ChessMatch
{
    public Table Table { get; private set; }
    public int Turn { get; private set; }
    public Color CurrentPlayer { get; private set; }
    public bool GameOver { get; private set; } = false;
    private HashSet<Piece> Pieces { get; } = new HashSet<Piece>();
    private HashSet<Piece> Captured { get; } = new HashSet<Piece>();
    public bool Check { get; private set; } = false;
    public Piece? VulnerableEnPassant { get; private set; }

    public ChessMatch()
    {
        VulnerableEnPassant = null;
        Table = new Table(8, 8);
        Turn = 1;
        CurrentPlayer = Color.White;
        InsertPieces();
    }

    public void RealizeMove(Position origin, Position destiny)
    {
        Piece capturedPiece = ExecuteMove(origin, destiny);
        if (IsInCheck(CurrentPlayer))
        {
            UndoMove(origin, destiny, capturedPiece);
            throw new TableException("You cannot put yourself in check!");
        }
        if (IsInCheck(Adversary(CurrentPlayer)))
        {
            Check = true;
        }
        else
        {
            Check = false;
        }

        if (CheckMate(Adversary(CurrentPlayer)))
        {
            GameOver = true;
        }
        else
        {
            IncrementTurn();
            ChangePlayer();
        }
        Piece piece = Table.Piece(destiny);
        //#Special move En Passant
        if (piece is Pawn && (destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2))
        {
            VulnerableEnPassant = piece;
        }
        else
        {
            VulnerableEnPassant = null;
        }
    }

    private void UndoMove(Position origin, Position destiny, Piece? capturedPiece)
    {
        Piece piece = Table.RemovePiece(destiny);
        piece.DecrementMoves();
        Table.PlacePiece(piece, origin);
        //undo Castling
        if (piece is King && destiny.Column == origin.Column + 2)
        {
            Position towerOrigin = new Position(origin.Line, origin.Column + 3);
            Position towerDestiny = new Position(origin.Line, origin.Column + 1);
            Piece tower = Table.RemovePiece(towerDestiny);
            tower.DecrementMoves();
            Table.PlacePiece(tower, origin);
        }

        if (piece is King && destiny.Column == origin.Column - 2)
        {
            Position towerOrigin = new Position(origin.Line, origin.Column - 4);
            Position towerDestiny = new Position(origin.Line, origin.Column - 1);
            Piece tower = Table.RemovePiece(towerDestiny);
            tower.DecrementMoves();
            Table.PlacePiece(tower, origin);
        }
        if (capturedPiece != null)
        {
            Table.PlacePiece(capturedPiece, destiny);
            Captured.Remove(capturedPiece);
        }
        // Special Move En Passant
        if (piece is Pawn)
        {
            if (origin.Column != destiny.Column && capturedPiece == VulnerableEnPassant)
            {
                Piece pawn = Table.RemovePiece(destiny);
                Position pawnPosition;
                if (piece.Color == Color.White)
                {
                    pawnPosition = new Position(3, destiny.Column);
                }
                else
                {
                    pawnPosition = new Position(4, destiny.Column);
                }
                Table.PlacePiece(pawn, pawnPosition);
            }
        }
    }

    private Piece ExecuteMove(Position origin, Position destiny)
    {
        Piece piece = Table.RemovePiece(origin);
        piece.IncrementMoves();
        Piece capturedPiece = Table.RemovePiece(destiny);
        Table.PlacePiece(piece, destiny);
        if (capturedPiece != null)
        {
            Captured.Add(capturedPiece);
        }
        //# Special Move Castling
        if (piece is King && destiny.Column == origin.Column + 2)
        {
            Position towerOrigin = new Position(origin.Line, origin.Column + 3);
            Position towerDestiny = new Position(origin.Line, origin.Column + 1);
            Piece tower = Table.RemovePiece(towerOrigin);
            tower.IncrementMoves();
            Table.PlacePiece(tower, towerDestiny);
        }
        //# Special Move Castling 2
        if (piece is King && destiny.Column == origin.Column - 2)
        {
            Position towerOrigin = new Position(origin.Line, origin.Column - 4);
            Position towerDestiny = new Position(origin.Line, origin.Column - 1);
            Piece tower = Table.RemovePiece(towerOrigin);
            tower.IncrementMoves();
            Table.PlacePiece(tower, towerDestiny);
        }
        //# Special Move En Passant
        if (piece is Pawn)
        {
            if (origin.Column != destiny.Column && capturedPiece == null)
            {
                Position pawnPosition;
                if (piece.Color == Color.White)
                {
                    pawnPosition = new Position(destiny.Line + 1, destiny.Column);
                }
                else
                {
                    pawnPosition = new Position(destiny.Line - 1, destiny.Column);
                }
                capturedPiece = Table.RemovePiece(pawnPosition);
                Captured.Add(capturedPiece);
            }
        }

        return capturedPiece;
    }
    public void ValidateOriginPosition(Position origin)
    {
        if (Table.Piece(origin) == null) throw new TableException("There's no piece in this position");
        if (CurrentPlayer != Table.Piece(origin).Color) throw new TableException("This piece does not belong to you");
        if (!Table.Piece(origin).AnyPossibleMove()) throw new TableException("There's no possible moves for this piece");
    }
    public void ValidateDestinyPosition(Position origin, Position destiny)
    {
        if (!Table.Piece(origin).PossibleMoviment(destiny)) throw new TableException("Destiny position is invalid");
    }

    public HashSet<Piece> CapturedPieces(Color color)
    {
        HashSet<Piece> output = new HashSet<Piece>();
        foreach (Piece piece in Captured)
        {
            if (piece.Color == color)
            {
                output.Add(piece);
            }
        }
        return output;
    }

    public HashSet<Piece> InGamePieces(Color color)
    {
        HashSet<Piece> output = new HashSet<Piece>();
        foreach (Piece piece in Pieces)
        {
            if (piece.Color == color)
            {
                output.Add(piece);
            }
        }
        output.ExceptWith(CapturedPieces(color));
        return output;
    }

    public bool CheckMate(Color color)
    {
        if (!IsInCheck(color))
        {
            return false;
        }

        foreach (Piece piece in InGamePieces(color))
        {
            bool[,] moves = piece.PossibleMoves();
            for (int i = 0; i < Table.Lines; i++)
            {
                for (int j = 0; j < Table.Columns; j++)
                {
                    if (moves[i, j])
                    {
                        Position origin = piece.Position;
                        Position destiny = new Position(i, j);
                        Piece capturedPiece = ExecuteMove(piece.Position, destiny);
                        bool inCheck = IsInCheck(color);
                        UndoMove(origin, destiny, capturedPiece);
                        if (!inCheck)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    private bool IsInCheck(Color color)
    {
        Piece king = King(color);
        if (king == null)
        {
            throw new TableException($"There's no king of the color {color} in the table");
        }
        int line = king.Position.Line;
        int column = king.Position.Column;

        foreach (Piece piece in InGamePieces(Adversary(color)))
        {
            bool[,] moves = piece.PossibleMoves();
            if (moves[line, column])
            {
                return true;
            }
        }
        return false;
    }

    private Piece King(Color color)
    {
        foreach (Piece piece in InGamePieces(color))
        {
            if (piece is King)
            {
                return piece;
            }
        }
        return null;
    }

    private Color Adversary(Color color)
    {
        return color == Color.White ? Color.Black : Color.White;
    }


    private void PlaceNewPiece(char column, int line, Piece piece)
    {
        Table.PlacePiece(piece, new ChessPosition(column, line).ToPosition());
        Pieces.Add(piece);
    }


    private void ChangePlayer()
    {
        CurrentPlayer = (Turn % 2 == 0) ? Color.Black : Color.White;
    }
    private void IncrementTurn()
    {
        Turn++;
    }

    private void InsertPieces()
    {
        PlaceNewPiece('a', 1, new Tower(Color.White, Table));
        PlaceNewPiece('h', 1, new Tower(Color.White, Table));
        PlaceNewPiece('b', 1, new Knight(Color.White, Table));
        PlaceNewPiece('g', 1, new Knight(Color.White, Table));
        PlaceNewPiece('c', 1, new Bishop(Color.White, Table));
        PlaceNewPiece('f', 1, new Bishop(Color.White, Table));
        PlaceNewPiece('d', 1, new Queen(Color.White, Table));
        PlaceNewPiece('e', 1, new King(Color.White, Table, this));
        char column = 'a';
        for (int i = 0; i < 8; i++)
        {
            PlaceNewPiece(column, 2, new Pawn(Color.White, Table, this));
            PlaceNewPiece(column, 7, new Pawn(Color.Black, Table, this));
            column++;
        }
        PlaceNewPiece('a', 8, new Tower(Color.Black, Table));
        PlaceNewPiece('h', 8, new Tower(Color.Black, Table));
        PlaceNewPiece('b', 8, new Knight(Color.Black, Table));
        PlaceNewPiece('g', 8, new Knight(Color.Black, Table));
        PlaceNewPiece('c', 8, new Bishop(Color.Black, Table));
        PlaceNewPiece('f', 8, new Bishop(Color.Black, Table));
        PlaceNewPiece('d', 8, new Queen(Color.Black, Table));
        PlaceNewPiece('e', 8, new King(Color.Black, Table, this));
    }
}
