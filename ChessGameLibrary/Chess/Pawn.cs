namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
public class Pawn : Piece
{
    public Pawn(Color color, Table table) : base (color, table){}

    public override string ToString()
    {
        const string white = "\u265f";
        const string black = "\u2659";
        string output = (this.Color == Color.Black) ? black : white;
        return output;
    }
}
