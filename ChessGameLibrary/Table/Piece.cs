namespace ConsoleChessLibrary.Table;

public class Piece
{
    public Position Position {get; set;}
    public Color Color {get; protected set;}
    public int MoveQuantity {get; protected set;}
    public Table Table {get; protected set;}

    public Piece(Position position, Color color, int moveQuantity, Table table)
    {
        Position = position;
        Color = color;
        MoveQuantity = moveQuantity;
        Table = table;
    }
}
