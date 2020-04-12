using System;

namespace WolfAndSheep
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declarar variáveis
            Square[,] board;
            board = new Square[8, 8];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i,j] = new Square(i, j);
                }
            }
        }
     }

    class Square
    {
        // Diz se pode mover para o próximo quadrado
        private static bool isPlayable;
        // Linha do tabuleiro
        private static int row;
        // Coluna do tabuleiro
        private static int column;

        public Square(int x, int y)
        {
            row = x;
            column = y;
        }
    }
    
}
