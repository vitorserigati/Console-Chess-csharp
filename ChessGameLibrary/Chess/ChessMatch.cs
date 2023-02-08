namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
using ConsoleChessLibrary.Table.Exception;
public class ChessMatch
{
    public Table Table { get; private set; }
    public int Turn { get; private set; }
    public Color CurrentPlayer { get; private set; }
    public bool GameOver { get; private set; } = false;

    public ChessMatch()
    {
        Table = new Table(8, 8);
        Turn = 1;
        CurrentPlayer = Color.White;
        InsertPieces();
    }

    public void RealizeMove(Position origin, Position destiny)
    {
        if (true)
        {
            ExecuteMove(origin, destiny);
            IncrementTurn();
            ChangePlayer();
        }
        else
        {
            RealizeMove(origin, destiny);
        }
    }

    private void ExecuteMove(Position origin, Position destiny)
    {
        Piece piece = Table.RemovePiece(origin);
        piece.IncrementMoves();
        Piece capturedPiece = Table.RemovePiece(destiny);
        Table.PlacePiece(piece, destiny);
    }
    public void ValidateOriginPosition(Position origin)
    {
        if(Table.Piece(origin) == null) throw new TableException("There's no piece in this position");
        if(CurrentPlayer != Table.Piece(origin).Color) throw new TableException("This piece does not belong to you");
        if(!Table.Piece(origin).AnyPossibleMove()) throw new TableException("There's no possible moves for this piece");
    }
    public void ValidateDestinyPosition(Position origin, Position destiny)
    {
        if(!Table.Piece(origin).CanMoveto(destiny)) throw new TableException("Destiny position is invalid"); 
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
        Table.PlacePiece(new Tower(Color.White, Table), new ChessPosition('a', 1).ToPosition());
        Table.PlacePiece(new Tower(Color.White, Table), new ChessPosition('h', 1).ToPosition());
        Table.PlacePiece(new Knight(Color.White, Table), new ChessPosition('b', 1).ToPosition());
        Table.PlacePiece(new Knight(Color.White, Table), new ChessPosition('g', 1).ToPosition());
        Table.PlacePiece(new Bishop(Color.White, Table), new ChessPosition('c', 1).ToPosition());
        Table.PlacePiece(new Bishop(Color.White, Table), new ChessPosition('f', 1).ToPosition());
        Table.PlacePiece(new Queen(Color.White, Table), new ChessPosition('d', 1).ToPosition());
        Table.PlacePiece(new King(Color.White, Table), new ChessPosition('e', 1).ToPosition());
        char line = 'a';
        for (int i = 0; i < 8; i++)
        {
            Table.PlacePiece(new Pawn(Color.White, Table), new ChessPosition(line, 2).ToPosition());
            Table.PlacePiece(new Pawn(Color.Black, Table), new ChessPosition(line, 7).ToPosition());
            line++;
        }
        Table.PlacePiece(new Tower(Color.Black, Table), new ChessPosition('a', 8).ToPosition());
        Table.PlacePiece(new Tower(Color.Black, Table), new ChessPosition('h', 8).ToPosition());
        Table.PlacePiece(new Knight(Color.Black, Table), new ChessPosition('b', 8).ToPosition());
        Table.PlacePiece(new Knight(Color.Black, Table), new ChessPosition('g', 8).ToPosition());
        Table.PlacePiece(new Bishop(Color.Black, Table), new ChessPosition('c', 8).ToPosition());
        Table.PlacePiece(new Bishop(Color.Black, Table), new ChessPosition('f', 8).ToPosition());
        Table.PlacePiece(new Queen(Color.Black, Table), new ChessPosition('d', 8).ToPosition());
        Table.PlacePiece(new King(Color.Black, Table), new ChessPosition('e', 8).ToPosition());
    }
}
