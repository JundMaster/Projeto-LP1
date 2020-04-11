using System;

namespace testing
{
    class Program
    {

        static void Main(string[] args)
        {
            // DECLARING VARIABLES
            int [,] chessboard; 
            chessboard = new int[8, 8];

            // PRINTS A MESSAGE
            Console.WriteLine("Wolf Square 1, 3, 5 or 7?");

            // USER INPUT TO GET WOLF SQUARE
            string aux = Console.ReadLine();

            // CONVERT INPUT TO INT
            int wolf = Convert.ToInt16(aux);

            // PRINTS CHESSBOARD
            Console.WriteLine("");
            Console.Write("    0  1  2  3  4  5  6  7");
            for (int i = 0; i < chessboard.GetLength(0); i++)
            {
                // PRINTS EVERY COLUMN
                Console.WriteLine("");
                Console.WriteLine("   -- -- -- -- -- -- -- --");
                for (int j = 0; j < chessboard.GetLength(1); j++)
                {
                    // EACH TIME THERE IS A NEW ROW
                    if (j == 0)
                    {
                        // WRITES ROW NUMBER
                        Console.Write($"{i}");
                        Console.Write($" |");

                        // ONLY FOR THE FIRST COLUMN
                        if (i % 2 == 0)
                            Console.Write($"||");
                        else
                            Console.Write($"  |");   
                    } 

                    // SETS WOLF'S INITIAL POSITION ON THE FIRST ROW
                    else if (i == 0 && j == wolf)
                        Console.Write("|WW|");
                    
                    // SETS SHEEPS INITIAL POSITION ON THE LAST ROW
                    // if on the last row and j is pair and = 0
                    else if (i == 7 && j % 2 == 0 || j == 0)
                        Console.Write("|SS|");


                    // PRINTS WHITE SQUARES
                    // when and j are pair
                    else if (i % 2 == 0 && j % 2 == 0)
                        Console.Write($"||");
                    // WHEN I AND J ARE IMPAIR
                    else if (i % 2 != 0 && j % 2 != 0)
                        Console.Write($"||");

                    // PRINTS EMPTY SQUARES
                    else
                        Console.Write($"|  |");        

                }
                // PRINTS | ON THE LAST COLUMN
                if (i % 2 != 0)
                    Console.Write($"|");
                Console.Write($" {i}");
            }
            // PRINTS NUMBERS ON THE LAST ROW
            Console.WriteLine("");
            Console.WriteLine("   -- -- -- -- -- -- -- --");
            Console.WriteLine("    0  1  2  3  4  5  6  7");
        }

    }

}