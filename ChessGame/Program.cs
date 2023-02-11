namespace ConsoleChess;

using ConsoleChessLibrary.Table;
using ConsoleChessLibrary.Table.Exception;
using ConsoleChessLibrary.Chess;
class Program
{
    static void Main(string[] args)
    {

        try
        {
            ChessMatch match = new ChessMatch();
            while (!match.GameOver)
            {
                try
                {
                    Screen.PrintTable(match.Table);
                    Screen.PrintMatch(match);
                    Console.Write("Write origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();
                    match.ValidateOriginPosition(origin);

                    Screen.PrintTable(match.Table, origin);

                    Console.Write("Write Destiny: ");

                    Position destiny = Screen.ReadChessPosition().ToPosition();
                    match.ValidateDestinyPosition(origin, destiny);

                    match.RealizeMove(origin, destiny);
                }
                catch (TableException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                    Console.Write("Press Any key to try again... ");
                    Console.ReadKey();
                }catch( IndexOutOfRangeException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong move detected! {0}", e.Message);
                    Console.ResetColor();
                    Console.Write("Press Any key to try again...");
                    Console.ReadKey();
                }
            }
            Screen.PrintMatch(match);
        }
        catch (TableException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
