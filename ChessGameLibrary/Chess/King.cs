namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
public class King : Piece
{
    public King(Color color, Table table) : base(color, table) { }

    public override string ToString()
    {
        const string white = "\u265a";
        const string black = "\u2654";
        string output = (this.Color == Color.Black) ? black : white;
        return output;
    }

    public override bool[,] PossibleMoves()
    {
        bool[,] mat = new bool[Table.Lines, Table.Columns];
        Position pos = new Position(0, 0);

        //Up 
        pos.DefineValues(Position.Line - 1, Position.Column);
        if (Table.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Column] = true;
        };

        // Up right
        pos.DefineValues(Position.Line - 1, Position.Column + 1);
        if (Table.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Column] = true;
        };

        //Right
        pos.DefineValues(Position.Line, Position.Column + 1);
        if (Table.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Column] = true;
        };

        // Down Right
        pos.DefineValues(Position.Line + 1, Position.Column + 1);
        if (Table.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Column] = true;
        };

        //Down
        pos.DefineValues(Position.Line + 1, Position.Column);
        if (Table.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Column] = true;
        };

        // Down Left
        pos.DefineValues(Position.Line + 1, Position.Column - 1);
        if (Table.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Column] = true;
        };

        //Left
        pos.DefineValues(Position.Line, Position.Column - 1);
        if (Table.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Column] = true;
        };

        //Up left
        pos.DefineValues(Position.Line - 1, Position.Column - 1);
        if (Table.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.Line, pos.Column] = true;
        };



        return mat;
    }
}
