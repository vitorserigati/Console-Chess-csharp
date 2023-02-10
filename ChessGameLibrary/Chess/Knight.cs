namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
public class Knight : Piece
{
    public Knight(Color color, Table table) : base(color, table) { }

    public override bool[,] PossibleMoves()
    {

        bool[,] moves = new bool[Table.Lines, Table.Columns];
        Position position = new Position(0, 0);

        position.DefineValues(Position.Line - 1, Position.Column - 2);
        if(Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        }

        position.DefineValues(Position.Line + 1, Position.Column - 2);
        if(Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        }

        position.DefineValues(Position.Line - 1, Position.Column + 2);
        if(Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        }

        position.DefineValues(Position.Line + 1, Position.Column + 2);
        if(Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        }
        position.DefineValues(Position.Line - 2, Position.Column - 1);
        if(Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        }
        position.DefineValues(Position.Line + 2, Position.Column - 1);
        if(Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        }
        position.DefineValues(Position.Line + 2, Position.Column + 1);
        if(Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        }
        position.DefineValues(Position.Line - 2 , Position.Column + 1);
        if(Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        }

        return moves;
    }

    public override string ToString()
    {
        const string white = "\u265e";
        const string black = "\u2658";
        string output = (this.Color == Color.Black) ? black : white;
        return output;
    }
}
