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

            // Criação de cada Square
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // Cria uma instância da classe Square para cada ponto no 
                    // tabuleiro
                    board[i,j] = new Square(i, j);

                    // Dá valores à row e column de cada instância do Square
                    board[i,j].row = i;
                    board[i,j].column = j;

                    // Se o Square for BRANCO não é playable
                    // Todos os pares (é branco)
                    if (i % 2 == 0 && j % 2 == 0)
                        board[i,j].isPlayable = false;
                    // Todos os ímpares (é branco)
                    else if (i % 2 != 0 && j % 2 != 0)
                        board[i,j].isPlayable = false;
                    // Resto dos quadrados (é preto)
                    else
                        board[i,j].isPlayable = true;
                }
            }


            // Exemplo para imprimir o wolf
            
            var x = Convert.ToInt16(Console.ReadLine());
            var y = Convert.ToInt16(Console.ReadLine());
            board[x,y].animal = "Wolf";


            // Imprime o tabuleiro
            Console.WriteLine("");
            Console.WriteLine("Numbers = Playable Squares");
            Console.WriteLine("Black Spaces = Unplayable Squares");
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < 8; j++)
                {
                    if (board[i,j].animal == "Wolf")
                        Console.Write("W ");

                    // Se não der para jogar imprime PRETO
                    else if (board[i,j].isPlayable == false)
                        Console.Write($"  ");
                    
                    // Se der para jogar imprime a linha e a coluna
                    else
                    {
                        Console.Write($"{board[i,j].row}");
                        Console.Write($"{board[i,j].column}");     
                    }
                }
            }

        }
     }

    class Square
    {
        // Diz se pode mover para o próximo quadrado
        public bool isPlayable;
        // Linha do tabuleiro
        public int row;
        // Coluna do tabuleiro
        public int column;
        // Animal que vai ocupar o quadrado
        public string animal;

        public Square(int x, int y)
        {
            row = x;
            column = y;

        }
    }
        
    
}
