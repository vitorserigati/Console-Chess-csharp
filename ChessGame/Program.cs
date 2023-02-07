namespace ConsoleChess;

using ConsoleChessLibrary.Table;
using ConsoleChessLibrary.Chess;
class Program
{
    static void Main(string[] args)
    {

        try{
            Table table = new Table(8, 8);

            table.PlacePiece(new Tower(Color.Black, table), new Position(0, 0));
            table.PlacePiece(new Tower(Color.White, table), new Position(1, 3));
            table.PlacePiece(new King(Color.Black, table), new Position(0,2));
            table.PlacePiece(new King(Color.White, table), new Position(3,5));
            Screen.PrintTable(table);
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.ReadKey();
    }
}
