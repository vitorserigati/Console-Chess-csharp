namespace ConsoleChess.Screen;
using ConsoleChessLibrary.Table;

class Screen
{
    public static void PrintTable(Table table)
    {
        for (int i = 0; i < table.Lines; i++)
        {
            for (int j = 0; j < table.Columns; i++)
            {
                if (table.Piece(i, j) == null)
                {
                    Console.Write(" - ");
                }
                else
                {
                    Console.Write($"{table.Piece(i, j)}, ");
                }
            }
        }
    }
}
