namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
public class Pawn : Piece
{
    private ChessMatch Match { get; }
    public Pawn(Color color, Table table, ChessMatch match) : base(color, table)
    {
        Match = match;
    }

    public override bool[,] PossibleMoves()
    {
        bool[,] moves = new bool[Table.Lines, Table.Columns];
        Position position = new Position(0, 0);
        Position inTheWayPosition = new Position(0, 0);

        if (Color == Color.White)
        {
            position.DefineValues(Position.Line - 1, Position.Column);
            if (Table.ValidPosition(position) && Free(position))
            {
                moves[position.Line, position.Column] = true;
            }
            position.DefineValues(Position.Line - 2, Position.Column);
            inTheWayPosition.DefineValues(Position.Line - 1, Position.Column);
            if (Table.ValidPosition(position) && Free(position) && Free(inTheWayPosition) && MoveQuantity == 0)
            {
                moves[position.Line, position.Column] = true;
            }
            position.DefineValues(Position.Line - 1, Position.Column - 1);
            if (Table.ValidPosition(position) && EnemyExistis(position))
            {
                moves[position.Line, position.Column] = true;
            }
            position.DefineValues(Position.Line - 1, Position.Column + 1);
            if (Table.ValidPosition(position) && EnemyExistis(position))
            {
                moves[position.Line, position.Column] = true;
            }
            // #Special Move En Passant
            if (Position.Line == 3)
            {
                Position left = new Position(Position.Line, Position.Column - 1);
                if (Table.ValidPosition(left) && EnemyExistis(left) && Table.Piece(left) == Match.VulnerableEnPassant)
                {
                    moves[left.Line - 1, left.Column] = true;
                }
                Position right = new Position(Position.Line, Position.Column + 1);
                if (Table.ValidPosition(right) && EnemyExistis(right) && Table.Piece(right) == Match.VulnerableEnPassant)
                {
                    moves[right.Line - 1, right.Column] = true;
                }
            }

        }
        else
        {

            position.DefineValues(Position.Line + 1, Position.Column);
            if (Table.ValidPosition(position) && Free(position))
            {
                moves[position.Line, position.Column] = true;
            }
            position.DefineValues(Position.Line + 2, Position.Column);
            inTheWayPosition.DefineValues(Position.Line + 1, Position.Column);
            if (Table.ValidPosition(position) && Free(position) && Free(inTheWayPosition) && MoveQuantity == 0)
            {
                moves[position.Line, position.Column] = true;
            }
            position.DefineValues(Position.Line + 1, Position.Column - 1);
            if (Table.ValidPosition(position) && EnemyExistis(position))
            {
                moves[position.Line, position.Column] = true;
            }
            position.DefineValues(Position.Line + 1, Position.Column + 1);
            if (Table.ValidPosition(position) && EnemyExistis(position))
            {
                moves[position.Line, position.Column] = true;
            }

            // #Special Move En Passant
            if (Position.Line == 4)
            {
                Position left = new Position(Position.Line, Position.Column - 1);
                if (Table.ValidPosition(left) && EnemyExistis(left) && Table.Piece(left) == Match.VulnerableEnPassant)
                {
                    moves[left.Line + 1, left.Column] = true;
                }
                Position right = new Position(Position.Line, Position.Column + 1);
                if (Table.ValidPosition(right) && EnemyExistis(right) && Table.Piece(right) == Match.VulnerableEnPassant)
                {
                    moves[right.Line + 1, right.Column] = true;
                }
            }
        }

        return moves;
    }

    public override string ToString()
    {
        const string white = "\u265f";
        const string black = "\u2659";
        string output = (this.Color == Color.Black) ? black : white;
        return output;
    }
    private bool EnemyExistis(Position position)
    {
        Piece piece = Table.Piece(position);
        return piece != null && piece.Color != Color;
    }

    private bool Free(Position position)
    {
        return Table.Piece(position) == null;
    }
}
