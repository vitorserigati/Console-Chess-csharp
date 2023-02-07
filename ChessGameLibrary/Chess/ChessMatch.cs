namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
public class ChessMatch
{
    public Table Table { get; private set; }
    private int Turn;
    private Color CurrentPlayer;
    public bool GameOver {get; private set; } = false;

    public ChessMatch()
    {
        Table = new Table(8, 8);
        Turn = 1;
        CurrentPlayer = Color.White;
        InsertPieces();
    }

    public void ExecuteMove(Position origin, Position destiny)
    {
        Piece piece = Table.RemovePiece(origin);
        piece.IncrementMoves();
        Piece capturedPiece = Table.RemovePiece(destiny);
        Table.PlacePiece(piece, destiny);
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
        char line= 'a';
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
