namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
public class Bishop : Piece
{
    public Bishop(Color color, Table table) : base (color, table){}

    public override string ToString()
    {
        const string white = "\u265d";
        const string black = "\u2657";
        string output = (this.Color == Color.Black) ? black : white;
        return output;
    }
}
