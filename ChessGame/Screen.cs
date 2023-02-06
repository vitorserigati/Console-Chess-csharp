namespace ConsoleChess;
using ConsoleChessLibrary.Table;

class Screen
{
    public static void PrintTable(Table table)
    {
        for (int i = 0; i < table.Lines; i++)
        {
            for (int j = 0; j < table.Columns; j++)
            {
                if (table.Piece(i, j) == null)
                {
                    Console.Write(" - ");
                }
                else
                {
                    Console.Write($" {table.Piece(i, j)} ");
                }
            }
            Console.WriteLine();
        }
    }
}
