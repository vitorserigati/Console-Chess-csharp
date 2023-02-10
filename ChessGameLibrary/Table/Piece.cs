namespace ConsoleChessLibrary.Table;

public abstract class Piece
{
    public Position Position { get; set; }
    public Color Color { get; protected set; }
    public int MoveQuantity { get; protected set; }
    public Table Table { get; protected set; }

    public Piece(Color color, Table table)
    {
        Position = null;
        Color = color;
        MoveQuantity = 0;
        Table = table;
    }
    public void IncrementMoves()
    {
        MoveQuantity++;
    }

    public void DecrementMoves()
    {
        MoveQuantity--;
    }
    public bool AnyPossibleMove()
    {
        bool[,] moves = PossibleMoves();
        for (int i = 0; i < Table.Lines; i++)
        {
            for (int j = 0; j < Table.Columns; j++)
            {
                if (moves[i, j]) return true;
            }
        }

        return false;
    }

    public bool PossibleMoviment(Position pos)
    {
        return PossibleMoves()[pos.Line, pos.Column];
    }

    protected bool CanMove(Position pos)
    {
        Piece piece = Table.Piece(pos);
        return piece == null || piece.Color != Color;
    }

    public abstract bool[,] PossibleMoves();
}
