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

    protected bool CanMove(Position pos)
    {
        Piece piece = Table.Piece(pos);
        return piece == null || piece.Color != Color;
    }

    public abstract bool[,] PossibleMoves();
}
