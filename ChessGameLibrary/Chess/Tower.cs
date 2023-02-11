
namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
public class Tower : Piece
{
    public Tower(Color color, Table table) : base(color, table) { }

    public override bool[,] PossibleMoves()
    {
        bool[,] mat = new bool[Table.Lines, Table.Columns];
        Position pos = new Position(0, 0);

        //Up
        pos.DefineValues(Position.Line - 1, Position.Column);
        while(Table.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Column] = true;
            if(Table.Piece(pos) != null && Table.Piece(pos).Color != Color)
            {
                break;
            }
            pos.Line -= 1;
        }

        //Down
        pos.DefineValues(Position.Line + 1, Position.Column);
        while(Table.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Column] = true;
            if(Table.Piece(pos) != null && Table.Piece(pos).Color != Color)
            {
                break;
            }
            pos.Line += 1;
        }

        //Left
        pos.DefineValues(Position.Line , Position.Column - 1);
        while(Table.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Column] = true;
            if(Table.Piece(pos) != null && Table.Piece(pos).Color != Color)
            {
                break;
            }
            pos.Column -= 1;
        }

        //Right
        pos.DefineValues(Position.Line , Position.Column + 1);
        while(Table.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Column] = true;
            if(Table.Piece(pos) != null && Table.Piece(pos).Color != Color)
            {
                break;
            }
            pos.Column += 1;
        }

        return mat;
    }

    public override string ToString()
    {
        const string white = "\u265c";
        const string black = "\u2656";
        string output = (this.Color == Color.Black) ? black : white;
        return output;
    }

}
