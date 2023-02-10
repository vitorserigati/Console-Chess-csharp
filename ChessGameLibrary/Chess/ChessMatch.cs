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

    public ChessMatch()
    {
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
    }

    private void UndoMove(Position origin, Position destiny, Piece? capturedPiece)
    {
        Piece piece = Table.RemovePiece(destiny);
        piece.DecrementMoves();
        if (capturedPiece != null)
        {
            Table.PlacePiece(capturedPiece, destiny);
            Captured.Remove(capturedPiece);
        }
        Table.PlacePiece(piece, origin);
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
        if (!Table.Piece(origin).CanMoveto(destiny)) throw new TableException("Destiny position is invalid");
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
        PlaceNewPiece('e', 1, new King(Color.White, Table));
        char column = 'a';
        for (int i = 0; i < 8; i++)
        {
            PlaceNewPiece(column, 2, new Pawn(Color.White, Table));
            PlaceNewPiece(column, 7, new Pawn(Color.Black, Table));
            column++;
        }
        PlaceNewPiece('a', 8, new Tower(Color.Black, Table));
        PlaceNewPiece('h', 8, new Tower(Color.Black, Table));
        PlaceNewPiece('b', 8, new Knight(Color.Black, Table));
        PlaceNewPiece('g', 8, new Knight(Color.Black, Table));
        PlaceNewPiece('c', 8, new Bishop(Color.Black, Table));
        PlaceNewPiece('f', 8, new Bishop(Color.Black, Table));
        PlaceNewPiece('d', 8, new Queen(Color.Black, Table));
        PlaceNewPiece('e', 8, new King(Color.Black, Table));
    }
}
