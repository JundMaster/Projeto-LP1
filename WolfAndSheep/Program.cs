using System.Linq.Expressions;
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
            Console.WriteLine("Both animals can only move" +
                " ONE square per turn");
            Console.WriteLine("Sheep can only move forward.");
            Console.WriteLine("The wolf can move forward and backwards.");
            Console.WriteLine("Only ONE sheep can move per turn.");
            Console.WriteLine("The animals can only move to numbered empty" + 
                " squares.");
            Console.WriteLine("The wolf must reach the bottom squares.");
            Console.WriteLine("The flock of sheep must block wolf's path.");
            Console.WriteLine("The flock of sheep win if they block wolf's" +
                " path.");
            Console.WriteLine("Players must type the square's number to move" +
                " the animal.");

            Console.WriteLine("");
            
            //Pede ao jogador a casa inicial do Lobo
            Console.WriteLine("-- Insert Wolf's Initial Square Number--");
            Console.WriteLine("-- 1     3     5    7 --");
        }

        private static void victory(string animal, int plays)
        {
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine($"Congratulations, {animal} won the game !!!");
            Console.WriteLine($"Number of plays = {plays}");
        }


        private static void game()
        {
            // Declarar variáveis
            // Cria uma array de Squares
            Square[,] board;
            board = new Square[8, 8];

            // Variável para saber quando o jogo está a correr
            bool gameOver = false;

            // Variável para o número de jogadas
            int numberOfPlays = 0;

            // Varíaveis temporárias para o input do jogador
            string aux1, aux2, aux3;

            // Variáveis para o input do jogador para o movimento do WOLF
            int wolfVertTemp = 0;
            int wolfHorzTemp = 0;

            // Variáveis para a posição do WOLF
            int wolfVert = 0;
            int wolfHorz = 0;


            // Variáveis para o movimento da SHEEP
            int sheep1Vert = 7;
            int sheep2Vert = 7;
            int sheep3Vert = 7;
            int sheep4Vert = 7;
            int sheep1Horz = 0;
            int sheep2Horz = 2;
            int sheep3Horz = 4;
            int sheep4Horz = 6;

            // Posições de vitória para o WOLF
            int[] wolfVictoryPositions = new int[4] {0, 2, 4, 6};

            const string errorMessage = "----------- Error Message -----------";
            

            while (gameOver == false)
            {
                // cria o board com a classe Square
                // Criação de cada Square
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        // Cria uma instância da classe Square para cada ponto  
                        // no tabuleiro
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


                
                // GAMEPLAY
                // Apenas para a primeira jogada no jogo
                if (numberOfPlays == 0)
                {
                    // Faz o loop do input do jogador ate colocar um 
                    // Square válido
                    do 
                    {
                        Console.WriteLine("");
                        
                        // Pedir input ao jogador
                        Console.Write("Insert a starting position: ");
                        aux2 = Console.ReadLine();
                        // Se escrever exit, sai do jogo
                        if (aux2 == "exit")
                        {
                            gameOver = true;
                            break;
                        }

                        //Converte a string do input para int
                        wolfHorz = Convert.ToInt16(aux2);

                        // Se meter o número valido, sai do loop
                        if (board[wolfVert, wolfHorz].isPlayable)
                        {
                            break;
                        }

                        // Se não for válido vai repetir o processo
                        Console.WriteLine(" ");
                        Console.WriteLine("Please choose a valid number");
                    } while(board[wolfVert, wolfHorz].isPlayable == false);
                }
                    

                // Jogadas para o WOLF
                // Quando o numero de jogadas é ímpar
                else if (numberOfPlays % 2 != 0)
                {
                    do 
                    {
                        // Define posição playable atual do WOLF como false
                        board[wolfVert, wolfHorz].isPlayable = false;
                        
                        Console.WriteLine(" ");
                        Console.WriteLine(" ");
                        Console.WriteLine("----------- WOLF TURN -----------");

                        // Pedir input ao jogador
                        Console.Write("Insert a vertical num: ");
                        aux1 = Console.ReadLine();
                        Console.Write("Insert a horizontal num: ");
                        aux2 = Console.ReadLine();
                        // Se escrever exit, sai do jogo
                        if (aux1 == "exit" || aux2 == "exit")
                        {
                            gameOver = true;
                            break;
                        }
                        
                        // Input temporário que serve para comparar com o antigo


                        try
                        {
                            wolfVertTemp = Convert.ToInt16(aux1);
                        }
                        // Aparece uma mensagem para inserir um número válido
                        // caso o jogador insira um número muito grande
                        catch (OverflowException)
                        {
                            Console.WriteLine(errorMessage);
                            Console.WriteLine("");
                            Console.WriteLine("Please insert a valid number.");
                            continue;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine(errorMessage);
                            Console.WriteLine("");
                            Console.WriteLine("The input must be a valid line number"+
                            "or the 'exit' command.");
                            continue;
                        }

                        try
                        {
                            wolfHorzTemp = Convert.ToInt16(aux2);
                        }
                        // Aparece uma mensagem para inserir um número válido
                        // caso o jogador insira um número muito grande
                        catch (OverflowException)
                        {
                            Console.WriteLine(errorMessage);
                            Console.WriteLine("");
                            Console.WriteLine("Please insert a valid number.");
                            continue;
                        }
                        
                        catch (FormatException)
                        {
                            Console.WriteLine(errorMessage);
                            Console.WriteLine("");
                            Console.WriteLine("The input must be a valid line number"+
                            "or the 'exit' command.");
                            continue;
                        }

                        
                        
                        
                        // Se meter o número valido, sai do loop
                        // Só aceita números com +1 ou -1 que a casa atual
                        if (wolfVertTemp >= 0 && wolfVertTemp <= 8 && 
                                wolfHorzTemp >= 0 && wolfHorzTemp <= 8 )
                            if (wolfVertTemp != wolfVert && 
                                    wolfHorzTemp != wolfHorz )
                                if (wolfVertTemp < wolfVert + 2 && 
                                        wolfHorzTemp < wolfHorz + 2)
                                    if (wolfVertTemp > wolfVert - 2 && 
                                            wolfHorzTemp > wolfHorz -2)
                                    {

                                        // Transforma as próximas coordernadas 
                                        // nas do input do utilizador
                                        if (board[wolfVertTemp, wolfHorzTemp].
                                                isPlayable)
                                        {
                                            // Define posição em que o WOLF 
                                            // estava como playable = TRUE
                                            board[wolfVert, wolfHorz].
                                                    isPlayable = true;
                                            // Define nova posição do lobo e
                                            // define como playable = False
                                            wolfVert = wolfVertTemp;
                                            wolfHorz = wolfHorzTemp;
                                            board[wolfVert, wolfHorz].
                                                isPlayable = false;
                                            break;
                                            
                                        }
                                    }
                        // Se não for válido vai repetir o loop
                        Console.WriteLine("Please choose a valid number");
                    } while(board[wolfVert, wolfHorz].isPlayable == false);
                }
                

                // Jogadas para A SHEEP
                // Quando o numero de jogadas é par
                else if (numberOfPlays % 2 == 0)
                {
                    do
                    {
                        // Nos turnos das ovelhas mete os isPlayable do
                        // WOLF a False
                        board[wolfVert, wolfHorz].isPlayable = false;

                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("----------- FLOCK TURN -----------");
                        Console.WriteLine("Choose a Sheep to play:\n");
                        Console.WriteLine("-- S1     S2     S3    S4 --");
                        aux3 = Console.ReadLine();
                        if(aux3 == "S1")
                        {
                            Console.Write("You chose sheep 1");
                            // Pedir input ao jogador
                            Console.Write("Insert a vertical num: ");
                            aux1 = Console.ReadLine();
                            Console.Write("Insert a horizontal num: ");
                            aux2 = Console.ReadLine();
                            // Se escrever exit, sai do jogo
                            if (aux1 == "exit" || aux2 == "exit")
                            {
                                gameOver = true;
                                break;
                            }
                            
                            //Converte a string do input para int
                            sheep1Vert = Convert.ToInt16(aux1);
                            sheep1Horz = Convert.ToInt16(aux2);

                            // Se meter o número valido, sai do loop
                            if (board[sheep1Vert, sheep1Horz].isPlayable)
                            {
                                break;
                            }
                            
                            // Se não for válido vai repetir o processo
                            Console.WriteLine("Please choose a valid number");
                            board[sheep1Vert, sheep1Horz].animal = "Sheep_01";
                        }

                        else if(aux3 == "S2")
                        {
                            Console.Write("You chose sheep 2");
                            // Pedir input ao jogador
                            Console.Write("Insert a vertical num: ");
                            aux1 = Console.ReadLine();
                            Console.Write("Insert a horizontal num: ");
                            aux2 = Console.ReadLine();
                            // Se escrever exit, sai do jogo
                            if (aux1 == "exit" || aux2 == "exit")
                            {
                                gameOver = true;
                                break;
                            }
                            
                            //Converte a string do input para int
                            sheep2Vert = Convert.ToInt16(aux1);
                            sheep2Horz = Convert.ToInt16(aux2);

                            // Se meter o número valido, sai do loop
                            if (board[sheep2Vert, sheep2Horz].isPlayable)
                                break;
                            
                            // Se não for válido vai repetir o processo
                            Console.WriteLine("Please choose a valid number");
                            board[sheep2Vert, sheep2Horz].animal = "Sheep_02";
                        }
                    
                        else if(aux3 == "S3")
                        {
                            Console.Write("You chose sheep 3");
                            // Pedir input ao jogador
                            Console.Write("Insert a vertical num: ");
                            aux1 = Console.ReadLine();
                            Console.Write("Insert a horizontal num: ");
                            aux2 = Console.ReadLine();
                            // Se escrever exit, sai do jogo
                            if (aux1 == "exit" || aux2 == "exit")
                            {
                                gameOver = true;
                                break;
                            }
                            
                            //Converte a string do input para int
                            sheep3Vert = Convert.ToInt16(aux1);
                            sheep3Horz = Convert.ToInt16(aux2);

                            // Se meter o número valido, sai do loop
                            if (board[sheep3Vert, sheep3Horz].isPlayable)
                                break;
                            
                            // Se não for válido vai repetir o processo
                            Console.WriteLine("Please choose a valid number");
                            board[sheep3Vert, sheep3Horz].animal = "Sheep_03";
                        }
                    
                        else if(aux3 == "S4")
                        {
                            Console.Write("You chose sheep 4");
                            // Pedir input ao jogador
                            Console.Write("Insert a vertical num: ");
                            aux1 = Console.ReadLine();
                            Console.Write("Insert a horizontal num: ");
                            aux2 = Console.ReadLine();
                            // Se escrever exit, sai do jogo
                            if (aux1 == "exit" || aux2 == "exit")
                            {
                                gameOver = true;
                                break;
                            }
                            
                            //Converte a string do input para int
                            sheep4Vert = Convert.ToInt16(aux1);
                            sheep4Horz = Convert.ToInt16(aux2);

                            // Se meter o número valido, sai do loop
                            if (board[sheep4Vert, sheep4Horz].isPlayable)
                                break;
                            
                            // Se não for válido vai repetir o processo
                            Console.WriteLine("Please choose a valid number");
                            board[sheep4Vert, sheep4Horz].animal = "Sheep_04";
                        }

                    } while(board[sheep1Vert, sheep1Horz].isPlayable == false
                        | board[sheep2Vert, sheep2Horz].isPlayable == false | 
                        board[sheep3Vert, sheep3Horz].isPlayable == false | 
                        board[sheep4Vert, sheep4Horz].isPlayable == false);

                }




                // Posições dos animais
                board[wolfVert, wolfHorz].animal = "Wolf";
                board[sheep1Vert, sheep1Horz].animal = "Sheep_01";
                board[sheep2Vert, sheep2Horz].animal = "Sheep_02";
                board[sheep3Vert, sheep3Horz].animal = "Sheep_03";
                board[sheep4Vert, sheep4Horz].animal = "Sheep_04";
                
                

                // So para testar ------------------------------------------------------------------------
                Console.WriteLine("");
                Console.WriteLine($"TESTE - WOLF PLAYABLE: {board[wolfVert, wolfHorz].isPlayable}, {board[wolfVert, wolfHorz].row}, {board[wolfVert, wolfHorz].column}");

                // Imprime o tabuleiro
                
                Console.WriteLine("");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Numbers = Playable Squares");
                Console.WriteLine("Black Spaces = Unplayable Squares");
                Console.WriteLine("exit  <- to quit the game");

                // Imprime números no topo do tabuleiro
                Console.WriteLine("     0 1 2 3 4 5 6 7");
                for (int i = 0; i < 8; i++)
                {
                    Console.WriteLine("");
                    Console.Write($"{i}   ");
                    for (int j = 0; j < 8; j++)
                    {
                        

                        // Imprime o WOLF em vez dos números e coloca esse mesmo
                        // Square.isPlayable como falso
                        if (board[i,j].animal == "Wolf")
                        {
                            Console.Write("WW");
                            // pode-se apagar no fim se nao houver problema >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> board[i,j].isPlayable = false;
                        }

                        // Imprime a SHEEP em vez dos números e coloca esse mesmo
                        // Square.isPlayable como falso
                        else if (board[i,j].animal == "Sheep_01" && board[i,j].
                        isPlayable)
                        {
                            Console.Write("S1");
                            // pode-se apagar no fim se nao houver problema >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> board[i,j].isPlayable = false;
                        }
                        else if (board[i,j].animal == "Sheep_02" && board[i,j].
                        isPlayable)
                        {
                            Console.Write("S2");
                            // pode-se apagar no fim se nao houver problema >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> board[i,j].isPlayable = false;
                        }
                        else if (board[i,j].animal == "Sheep_03" && board[i,j].
                        isPlayable)
                        {
                            Console.Write("S3");
                            // pode-se apagar no fim se nao houver problema >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> board[i,j].isPlayable = false;
                        }
                        else if (board[i,j].animal == "Sheep_04" && board[i,j].
                        isPlayable)
                        {
                            Console.Write("S4");
                            // pode-se apagar no fim se nao houver problema >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> board[i,j].isPlayable = false;
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
                    // Imprime os números à direita do tabuleiro
                    Console.Write($"   {i}");
                }
                // Imprime números em baixo do tabuleiro
                Console.WriteLine("\n\n     0 1 2 3 4 5 6 7");



                // Incrementa número de jogadas
                numberOfPlays += 1;


                // Mensagem de vitória
                // Vai detetar se o Wolf está num dos últimos Squares
                foreach (int x in wolfVictoryPositions)
                {
                    if (board[7,x].animal == "Wolf")
                    {
                        victory("Wolf", numberOfPlays);
                        gameOver = true;
                    }
                }
            }
        }
     }
    

    class Square
    {
        // Diz se o square pode ser jogado ou não
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