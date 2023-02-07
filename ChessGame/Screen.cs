namespace ConsoleChess;
using ConsoleChessLibrary.Table;

class Screen
{
    public static void PrintTable(Table table)
    {
        for (int i = 0; i < table.Lines; i++)
        {
            Console.Write( 8 - i + " ");
            for (int j = 0; j < table.Columns; j++)
            {
                if (table.Piece(i, j) == null)
                {
                    Console.Write(" - ");
                }
                else
                {
                    PrintPiece(table.Piece(i, j));
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("   a  b  c  d  e  f  g  h ");
    }

    public static void PrintPiece(Piece piece){
        if(piece.Color == Color.White) {
            Console.Write(" " + piece + " ");
            return;
        }
        ConsoleColor aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(" " + piece + " ");
        Console.ForegroundColor = aux;
    }
}
