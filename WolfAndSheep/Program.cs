using System;

namespace WolfAndSheep
{
    class Program
    {
        static void Main(string[] args)
        {
            // Chama função de introdução
            intro();

            //Chama a função que inicia o jogo em si
            game();

        }

        private static void intro()

        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Both animals can only move ONE square per turn");
            Console.WriteLine("Sheep can only move forward.");
            Console.WriteLine("The wolf can move forward and backwards.");
            Console.WriteLine("Only ONE sheep can move per turn.");
            Console.WriteLine("The animals can only move to numbered empty squares.");
            Console.WriteLine("The wolf must reach the bottom squares.");
            Console.WriteLine("The flock of sheep must block wolf's path.");
            Console.WriteLine("The flock of sheep win if they block wolf's path.");
            Console.WriteLine("Players must type the square's number to move the animal.");

            Console.WriteLine("");
            
            //Pede ao jogador a casa inicial do Lobo
            Console.WriteLine("-- Insert Wolf's Initial Square Number--");
            Console.WriteLine("-- 1     3     5    7 --");
        }

        private static void victory(string animal)
        {
            Console.WriteLine("");
            Console.WriteLine($"Congratulations, {animal} won the game !!!");
        }


        private static void game()
        {
            // Declarar variáveis
            Square[,] board;
            string aux;
            int wolfPos;
            int[] wolfVictoryPositions = new int[4] {0, 2, 4, 6};

            // cria o board com a classe Square
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

            // Faz o loop do input do jogador ate colocar um Square válido
            do 
            {
                // Variável utilizada para guardar a posição escolhida
                aux = Console.ReadLine();
                //Converte a string do input para int
                wolfPos = Convert.ToInt16(aux);
                if (board[0,wolfPos].isPlayable)
                    break;
                Console.WriteLine("Please choose a valid number");
            } while(board[0,wolfPos].isPlayable == false);

            // Posições dos animais
            board[0,wolfPos].animal = "Wolf";
            board[7,0].animal = "Sheep";
            board[7,2].animal = "Sheep";
            board[7,4].animal = "Sheep";
            board[7,6].animal = "Sheep";


            // Imprime o tabuleiro
            Console.WriteLine("");
            Console.WriteLine("Numbers = Playable Squares");
            Console.WriteLine("Black Spaces = Unplayable Squares");
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < 8; j++)
                {
                    // Imprime o WOLF em vez dos números e coloca esse mesmo
                    // Square.isPlayable como falso
                    if (board[i,j].animal == "Wolf" && board[i,j].isPlayable)
                    {
                        Console.Write("WW");
                        board[i,j].isPlayable = false;
                    }

                    // Imprime a SHEEP em vez dos números e coloca esse mesmo
                    // Square.isPlayable como falso
                    else if (board[i,j].animal == "Sheep" && board[i,j].
                    isPlayable)
                    {
                        Console.Write("SH");
                        board[i,j].isPlayable = false;
                    }

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

            // Mensagem de vitória
            // Vai detetar se o Wolf está num dos últimos Squares
            foreach (int x in wolfVictoryPositions)
                if (board[7,x].animal == "Wolf")
                    victory("Wolf");
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