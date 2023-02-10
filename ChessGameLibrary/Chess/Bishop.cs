namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
public class Bishop : Piece
{
    public Bishop(Color color, Table table) : base(color, table) { }

    public override bool[,] PossibleMoves()
    {
        bool[,] moves = new bool[Table.Lines, Table.Columns];
        Position position = new Position(0, 0);

        //Up Left
        position.DefineValues(Position.Line - 1, Position.Column - 1);
        while (Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
            if (Table.Piece(position) != null && Table.Piece(position).Color != Color)
            {
                break;
            }
            position.DefineValues(position.Line - 1, position.Column - 1);
        }

        //Up Right
        position.DefineValues(Position.Line - 1, Position.Column + 1);
        while (Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
            if (Table.Piece(position) != null && Table.Piece(position).Color != Color)
            {
                break;
            }
            position.DefineValues(position.Line - 1, position.Column + 1);
        }

        //Down Left
        position.DefineValues(Position.Line + 1, Position.Column - 1);
        while (Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
            if (Table.Piece(position) != null && Table.Piece(position).Color != Color)
            {
                break;
            }
            position.DefineValues(position.Line + 1, position.Column - 1);
        }

        //Down Right
        position.DefineValues(Position.Line + 1, Position.Column + 1);
        while (Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
            if (Table.Piece(position) != null && Table.Piece(position).Color != Color)
            {
                break;
            }
            position.DefineValues(position.Line + 1, position.Column + 1);
        }
        
        return moves;
    }

    public override string ToString()
    {
        const string white = "\u265d";
        const string black = "\u2657";
        string output = (this.Color == Color.Black) ? black : white;
        return output;
    }
}
