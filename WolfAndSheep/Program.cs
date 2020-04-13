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
            Console.WriteLine("");
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

            // Variáveis para o input do jogador para o movimento do WOLF
            int sheep1VertTemp = 0;
            int sheep1HorzTemp = 0;
            int sheep2VertTemp = 0;
            int sheep2HorzTemp = 0;
            int sheep3VertTemp = 0;
            int sheep3HorzTemp = 0;
            int sheep4VertTemp = 0;
            int sheep4HorzTemp = 0;
            
            // Posições de vitória para o WOLF
            int[] wolfVictoryPositions = new int[4] {0, 2, 4, 6};
            

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
                        
                        Console.WriteLine("");
                        Console.WriteLine("");
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
                        wolfVertTemp = Convert.ToInt16(aux1);
                        wolfHorzTemp = Convert.ToInt16(aux2);
                        
                        // Se meter o número valido, sai do loop
                        // Só aceita números com +1 ou -1 que a casa atual
                        if (wolfVertTemp >= 0 && wolfVertTemp <= 9 && 
                                wolfHorzTemp >= 0 && wolfHorzTemp <= 9 )
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
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("----------- FLOCK TURN -----------");
                    Console.WriteLine("Choose a Sheep to play:\n");
                    Console.WriteLine("-- S1     S2     S3    S4 --");
                    aux3 = Console.ReadLine();
                    Console.Write($"You chose {aux3}\n");
                    do
                    {
                        
                        
                        if(aux3 == "S1")
                        {
                            
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
                            // Só aceita números com +1 ou -1 na Horizontal
                            // e -1 na Vertical que a casa atual
                            if (sheep1VertTemp >= 0 && sheep1VertTemp <= 8 && 
                                sheep1HorzTemp >= 0 && sheep1HorzTemp <= 8 )
                            if (sheep1VertTemp != sheep1Vert && 
                                    sheep1HorzTemp != sheep1Horz )
                                if (sheep1VertTemp < sheep1Vert + 2 && 
                                        sheep1HorzTemp < sheep1Horz + 2)
                                    if (sheep1VertTemp > sheep1Vert - 2 && 
                                            sheep1HorzTemp > sheep1Horz -2)   
                                    {

                                        // Transforma as próximas coordernadas 
                                        // nas do input do utilizador
                                        if (board[sheep1VertTemp, sheep1HorzTemp].
                                                isPlayable)
                                        {
                                            // Define posição em que o WOLF 
                                            // estava como playable = TRUE
                                            board[sheep1Vert, sheep1Horz].
                                                    isPlayable = true;
                                            // Define nova posição do lobo e
                                            // define como playable = False
                                            sheep1Vert = sheep1VertTemp;
                                            sheep1Horz = sheep1HorzTemp;
                                            board[sheep1Vert, sheep1Horz].
                                                isPlayable = false;
                                            break;
                                            
                                        }
                                    }                         
                        }

                        else if(aux3 == "S2")
                        {
                            
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
                            // Só aceita números com +1 ou -1 na Horizontal
                            // e -1 na Vertical que a casa atual
                            if (sheep2VertTemp >= 0 && sheep2VertTemp <= 8 && 
                                sheep2HorzTemp >= 0 && sheep2HorzTemp <= 8 )
                            if (sheep2VertTemp != sheep2Vert && 
                                    sheep2HorzTemp != sheep2Horz )
                                if (sheep2VertTemp < sheep2Vert + 2 && 
                                        sheep2HorzTemp < sheep2Horz + 2)
                                    if (sheep2VertTemp > sheep2Vert - 2 && 
                                            sheep2HorzTemp > sheep2Horz -2)  
                                    {

                                        // Transforma as próximas coordernadas 
                                        // nas do input do utilizador
                                        if (board[sheep2VertTemp, sheep2HorzTemp].
                                                isPlayable)
                                        {
                                            // Define posição em que o WOLF 
                                            // estava como playable = TRUE
                                            board[sheep2Vert, sheep2Horz].
                                                    isPlayable = true;
                                            // Define nova posição do lobo e
                                            // define como playable = False
                                            sheep2Vert = sheep2VertTemp;
                                            sheep2Horz = sheep2HorzTemp;
                                            board[sheep2Vert, sheep2Horz].
                                                isPlayable = false;
                                            break;
                                            
                                        }
                                    }                         
                            
                        }
                    
                        else if(aux3 == "S3")
                        {
                            
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
                            // Só aceita números com +1 ou -1 na Horizontal
                            // e -1 na Vertical que a casa atual
                            if (sheep3VertTemp >= 0 && sheep3VertTemp <= 8 && 
                                sheep3HorzTemp >= 0 && sheep3HorzTemp <= 8 )
                            if (sheep3VertTemp != sheep3Vert && 
                                    sheep3HorzTemp != sheep3Horz )
                                if (sheep3VertTemp < sheep3Vert + 2 && 
                                        sheep3HorzTemp < sheep3Horz + 2)
                                    if (sheep3VertTemp > sheep3Vert - 2 && 
                                            sheep3HorzTemp > sheep3Horz -2)   
                                    {

                                        // Transforma as próximas coordernadas 
                                        // nas do input do utilizador
                                        if (board[sheep3VertTemp, sheep3HorzTemp].
                                                isPlayable)
                                        {
                                            // Define posição em que o WOLF 
                                            // estava como playable = TRUE
                                            board[sheep3Vert, sheep3Horz].
                                                    isPlayable = true;
                                            // Define nova posição do lobo e
                                            // define como playable = False
                                            sheep3Vert = sheep3VertTemp;
                                            sheep3Horz = sheep3HorzTemp;
                                            board[sheep3Vert, sheep3Horz].
                                                isPlayable = false;
                                            break;
                                            
                                        }
                                    }                         
                            

                        }
                    
                        else if(aux3 == "S4")
                        {
                            
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
                            // Só aceita números com +1 ou -1 na Horizontal
                            // e -1 na Vertical que a casa atual
                            if (sheep4VertTemp >= 0 && sheep4VertTemp <= 8 && 
                                sheep4HorzTemp >= 0 && sheep4HorzTemp <= 8 )
                            if (sheep4VertTemp != sheep4Vert && 
                                    sheep4HorzTemp != sheep4Horz )
                                if (sheep4VertTemp < sheep4Vert + 2 && 
                                        sheep4HorzTemp < sheep4Horz + 2)
                                    if (sheep4VertTemp > sheep4Vert - 2 && 
                                            sheep4HorzTemp > sheep4Horz -2)   
                                    {

                                        // Transforma as próximas coordernadas 
                                        // nas do input do utilizador
                                        if (board[sheep4VertTemp, sheep4HorzTemp].
                                                isPlayable)
                                        {
                                            // Define posição em que o WOLF 
                                            // estava como playable = TRUE
                                            board[sheep4Vert, sheep4Horz].
                                                    isPlayable = true;
                                            // Define nova posição do lobo e
                                            // define como playable = False
                                            sheep4Vert = sheep4VertTemp;
                                            sheep4Horz = sheep1HorzTemp;
                                            board[sheep4Vert, sheep4Horz].
                                                isPlayable = false;
                                            break;
                                            
                                        }
                                    }                         
                        }

                        Console.WriteLine("Please choose a valid number");
                    } while(board[sheep1VertTemp, sheep1HorzTemp].isPlayable == false
                        | board[sheep2VertTemp, sheep2HorzTemp].isPlayable == false | 
                        board[sheep3VertTemp, sheep3HorzTemp].isPlayable == false | 
                        board[sheep4VertTemp, sheep4HorzTemp].isPlayable == false);

                }




                // Posições dos animais
                board[wolfVert, wolfHorz].animal = "Wolf";
                board[sheep1Vert, sheep1Horz].animal = "Sheep_01";
                board[sheep2Vert, sheep2Horz].animal = "Sheep_02";
                board[sheep3Vert, sheep3Horz].animal = "Sheep_03";
                board[sheep4Vert, sheep4Horz].animal = "Sheep_04";
                
                

                

                // Imprime o tabuleiro
                
                Console.WriteLine("");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Numbers = Playable Squares");
                Console.WriteLine("Black Spaces = Unplayable Squares");
                Console.WriteLine("exit  <- to quit the game");

                // So para testar ------------------------------------------------------------------------
                Console.WriteLine("");
                Console.WriteLine($"TESTE - WOLF PLAYABLE: {board[wolfVert, wolfHorz].isPlayable}, {board[wolfVert, wolfHorz].row}, {board[wolfVert, wolfHorz].column}");
                
                for (int i = 0; i < 8; i++)
                {
                    Console.WriteLine("");
                    for (int j = 0; j < 8; j++)
                    {
                        // Imprime o WOLF em vez dos números e coloca esse mesmo
                        // Square.isPlayable como falso
                        if (board[i,j].animal == "Wolf")
                        {
                            Console.Write("WW");
                            board[i,j].isPlayable = false;
                        }

                        // Imprime a SHEEP em vez dos números e coloca esse mesmo
                        // Square.isPlayable como falso
                        else if (board[i,j].animal == "Sheep_01" && board[i,j].
                        isPlayable)
                        {
                            Console.Write("S1");
                            board[i,j].isPlayable = false;
                        }
                        else if (board[i,j].animal == "Sheep_02" && board[i,j].
                        isPlayable)
                        {
                            Console.Write("S2");
                            board[i,j].isPlayable = false;
                        }
                        else if (board[i,j].animal == "Sheep_03" && board[i,j].
                        isPlayable)
                        {
                            Console.Write("S3");
                            board[i,j].isPlayable = false;
                        }
                        else if (board[i,j].animal == "Sheep_04" && board[i,j].
                        isPlayable)
                        {
                            Console.Write("S4");
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