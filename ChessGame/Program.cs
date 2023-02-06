namespace ConsoleChess;

using ConsoleChessLibrary.Table;
using ConsoleChessLibrary.Chess;
class Program
{
    static void Main(string[] args)
    {
        Table table = new Table(8, 8);
        Screen.PrintTable(table);

        table.SetPiece(new Tower(Color.Black, table), new Position(0, 1));
        table.SetPiece(new Tower(Color.White, table), new Position(0, 2));
        table.SetPiece(new King(Color.Black, table), new Position(0, 3));
        table.SetPiece(new King(Color.White, table), new Position(0, 4));

        Screen.PrintTable(table);
        Console.ReadKey();
    }
}
