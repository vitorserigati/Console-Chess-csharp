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
                    Console.WriteLine();
                    Console.WriteLine($"Turn: {match.Turn}");
                    Console.WriteLine($"Waiting for {match.CurrentPlayer}'s move");
                    Screen.PrintCapturedPieces(match);

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
                }
            }
        }
        catch (TableException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
