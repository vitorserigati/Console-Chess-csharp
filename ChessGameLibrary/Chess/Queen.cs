namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
public class Queen : Piece
{
    public Queen(Color color, Table table) : base (color, table){}

    public override bool[,] PossibleMoves()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        const string white = "\u265b";
        const string black = "\u2655";
        string output = (this.Color == Color.Black) ? black : white;
        return output;
    }
}
