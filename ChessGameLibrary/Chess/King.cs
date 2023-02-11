namespace ConsoleChessLibrary.Chess;
using ConsoleChessLibrary.Table;
public class King : Piece
{
    private ChessMatch Match;
    public King(Color color, Table table, ChessMatch match) : base(color, table)
    {
        Match = match;
    }

    public override string ToString()
    {
        const string white = "\u265a";
        const string black = "\u2654";
        string output = (this.Color == Color.Black) ? black : white;
        return output;
    }

    public override bool[,] PossibleMoves()
    {
        bool[,] moves = new bool[Table.Lines, Table.Columns];
        Position position = new Position(0, 0);

        //Up 
        position.DefineValues(Position.Line - 1, Position.Column);
        if (Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        };

        // Up right
        position.DefineValues(Position.Line - 1, Position.Column + 1);
        if (Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        };

        //Right
        position.DefineValues(Position.Line, Position.Column + 1);
        if (Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        };

        // Down Right
        position.DefineValues(Position.Line + 1, Position.Column + 1);
        if (Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        };

        //Down
        position.DefineValues(Position.Line + 1, Position.Column);
        if (Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        };

        // Down Left
        position.DefineValues(Position.Line + 1, Position.Column - 1);
        if (Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        };

        //Left
        position.DefineValues(Position.Line, Position.Column - 1);
        if (Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        };

        //Up left
        position.DefineValues(Position.Line - 1, Position.Column - 1);
        if (Table.ValidPosition(position) && CanMove(position))
        {
            moves[position.Line, position.Column] = true;
        };

        // #Special Castling

        if (MoveQuantity == 0 && !Match.Check)
        {
            Position positionTower = new Position(Position.Line, Position.Column + 3);
            if (TestTowerForCastlingMove(positionTower))
            {
                Position positionOne = new Position(Position.Line, Position.Column + 1);
                Position positionTwo = new Position(Position.Line, Position.Column + 2);
                if (Table.Piece(positionOne) == null && Table.Piece(positionTwo) == null)
                {
                    moves[Position.Line, Position.Column + 2] = true;
                }
            }
        }
        //#Special Castling 2
        if (MoveQuantity == 0 && !Match.Check)
        {
            Position positionTower = new Position(Position.Line, Position.Column - 4);
            if (TestTowerForCastlingMove(positionTower))
            {
                Position positionOne = new Position(Position.Line, Position.Column - 1);
                Position positionTwo = new Position(Position.Line, Position.Column - 2);
                Position positionThree = new Position(Position.Line, Position.Column - 3);
                if (Table.Piece(positionOne) == null && Table.Piece(positionTwo) == null && Table.Piece(positionThree) == null)
                {
                    moves[Position.Line, Position.Column - 2] = true;
                }
            }
        }


        return moves;
    }

    private bool TestTowerForCastlingMove(Position position)
    {
        Piece piece = Table.Piece(position);
        return piece != null && piece is Tower && piece.Color == Color && piece.MoveQuantity == 0;
    }

}
