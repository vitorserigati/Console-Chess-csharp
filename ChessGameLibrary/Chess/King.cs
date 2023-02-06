namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
public class King : Piece
{
    public King(Color color, Table table) : base (color, table){}

    public override string ToString()
    {
        const string white = "\u265a";
        const string black = "\u2654";
        string output = (this.Color == Color.Black) ? black : white;
        return output;
    }
}
