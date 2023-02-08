namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
public class Knight : Piece
{
    public Knight(Color color, Table table) : base (color, table){}

    public override bool[,] PossibleMoves()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        const string white = "\u265e";
        const string black = "\u2658";
        string output = (this.Color == Color.Black) ? black : white;
        return output;
    }
}
