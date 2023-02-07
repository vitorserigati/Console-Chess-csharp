namespace ConsoleChess;

using ConsoleChessLibrary.Table;
using ConsoleChessLibrary.Table.Exception;
using ConsoleChessLibrary.Chess;
class Program
{
    static void Main(string[] args)
    {

        try{
            ChessMatch match = new ChessMatch();
            while(!match.GameOver){
                Console.Clear();
            Screen.PrintTable(match.Table);

            Console.Write("Write origin: ");
            Position origin = Screen.ReadChessPosition().ToPosition();
            Console.Write("Write Destiny: ");

            Position destiny = Screen.ReadChessPosition().ToPosition();

            match.ExecuteMove(origin, destiny);
            }
        }catch(TableException e)
        {
            Console.WriteLine(e.Message);
        }
        Console.WriteLine();
        Console.Write("Press any button to exit...");
        Console.ReadKey();
    }
}
