namespace ConsoleChess;
using ConsoleChessLibrary.Table;
using ConsoleChessLibrary.Chess;
using System.Text;

class Screen
{
    public static void PrintTable(Table table)
    {
        Console.Clear();
        for (int i = 0; i < table.Lines; i++)
        {
            Console.Write(8 - i + " ");
            for (int j = 0; j < table.Columns; j++)
            {
                PrintPiece(table.Piece(i, j));
            }
            Console.WriteLine();
        }
        Console.WriteLine("   a  b  c  d  e  f  g  h ");
        Console.WriteLine();
    }

    public static void PrintTable(Table table, Position position)
    {
        Console.Clear();
        ConsoleColor originalBackground = Console.BackgroundColor;
        ConsoleColor newBackground = ConsoleColor.DarkGray;
        bool[,] possiblePositions = table.Piece(position).PossibleMoves();

        for (int i = 0; i < table.Lines; i++)
        {
            Console.Write(8 - i + " ");
            for (int j = 0; j < table.Columns; j++)
            {
                if (possiblePositions[i, j])
                {
                    Console.BackgroundColor = newBackground;
                }
                else
                {
                    Console.BackgroundColor = originalBackground;
                }
                PrintPiece(table.Piece(i, j));
                Console.BackgroundColor = originalBackground;
            }
            Console.WriteLine();
        }
        Console.WriteLine("   a  b  c  d  e  f  g  h ");
        Console.BackgroundColor = originalBackground;
        Console.WriteLine();
    }

    public static void PrintPiece(Piece piece)
    {
        if (piece == null)
        {
            Console.Write(" - ");
        }
        else
        {
            if (piece.Color == Color.White)
            {
                Console.Write(" " + piece + " ");
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(" " + piece + " ");
                Console.ForegroundColor = aux;
            }
        }
    }


    public static ChessPosition ReadChessPosition()
    {
        string s = Console.ReadLine();
        char ch = s[0];
        int line = int.Parse(s[1] + "");
        return new ChessPosition(ch, line);
    }

    public static void PrintCapturedPieces(ChessMatch match)
    {
        Console.WriteLine("Captured Pieces: ");
        Console.Write("White: ");
        PrintPiecesCollection(match.CapturedPieces(Color.White));
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Black: ");
        PrintPiecesCollection(match.CapturedPieces(Color.Black));
        Console.ResetColor();
    }

    private static void PrintPiecesCollection(HashSet<Piece> collection)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append('[');
        foreach( Piece piece in collection)
        {
            sb.Append(piece);
            sb.Append(' ');
        }
        sb.Append(']');
        sb.AppendLine();
        Console.WriteLine(sb.ToString());
    }
}
