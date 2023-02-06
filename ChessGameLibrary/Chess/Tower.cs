
namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
public class Tower : Piece
{
    public Tower(Color color, Table table) : base (color, table){}

    public override string ToString()
    {
        const string white = "\u265c";
        const string black = "\u2656";
        string output = (this.Color == Color.Black) ? black : white;
        return output;
    }
}
