namespace ConsoleChess;

using ConsoleChessLibrary.Table;
using ConsoleChessLibrary.Chess;
class Program
{
    static void Main(string[] args)
    {
        Table table = new Table(8, 8);
        Screen.PrintTable(table);

        table.PlacePiece(new Tower(Color.Black, table), new Position(0, 1));
        table.PlacePiece(new Tower(Color.White, table), new Position(0, 2));
        table.PlacePiece(new King(Color.Black, table), new Position(0, 3));
        table.PlacePiece(new King(Color.White, table), new Position(0, 4));

        Screen.PrintTable(table);
        Console.ReadKey();
    }
}
