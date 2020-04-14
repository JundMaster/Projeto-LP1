using System;

namespace WolfAndSheep
{
    class Program
    {
            // Variáveis para o input do jogador para o movimento do WOLF
           static int [] wolfTempPos = new int[2] {0, 0};

           // Variáveis para a posição atual do WOLF
            static int [] wolfPos = new int[2] {0, 0};

            // Declarar variáveis
            // Cria uma array de Squares
            static Square[,] board;

            // Variáveis para a posição atual da Sheep
            private static int [] sheep1Pos = new int[2] {2, 1};
            private static int [] sheep2Pos = new int[2] {1, 2};
            private static int [] sheep3Pos = new int[2] {7, 4};
            private static int [] sheep4Pos = new int[2] {7, 6};

            // Variáveis para o input do jogador para o movimento da Sheep
            private static int [] sheepTempPos = new int[2] {0, 0};
            private static int [] sheepNewPos = new int[2] {0, 0};
           
        static void Main(string[] args)
        {

            // Chama função de introdução
            intro();

            //Chama a função que inicia o jogo em si
            game();

        }

        private static void game()
        {
            /* // Declarar variáveis
            // Cria uma array de Squares
            Square[,] board;*/
            board = new Square[8, 8]; 

            // Variável para saber quando o jogo está a correr
            bool gameOver = false;

            // Variável para o número de jogadas
            int numberOfPlays = 0;

            // Varíaveis temporárias para o input do jogador
            string aux1, aux2, aux3;

           /*  // Variáveis para o input do jogador para o movimento do WOLF
            int [] wolfTempPos = new int[2] {0, 0}; */

            /* // Variáveis para a posição atual do WOLF
            int [] wolfPos = new int[2] {0, 0}; */

            // Todos os quadrados playable à volta do lobo
            int wolfNextRow = wolfPos[0] +1;
            int wolfNextColumn = wolfPos[1] +1;
            int wolfNextRowNeg = wolfPos[0] -1;
            int wolfNextColumnNeg = wolfPos[1] -1;

            /* // Variáveis para o input do jogador para o movimento da Sheep
            int [] sheepTempPos = new int[2] {0, 0};
            int [] sheepNewPos = new int[2] {0, 0}; */

            /* // Variáveis para a posição atual da Sheep
            int [] sheep1Pos = new int[2] {2, 1};
            int [] sheep2Pos = new int[2] {1, 2};
            int [] sheep3Pos = new int[2] {7, 4};
            int [] sheep4Pos = new int[2] {7, 6}; */
           
            // Posições de vitória para o WOLF
            int[] wolfVictoryPositions = new int[4] {0, 2, 4, 6};

            // Mensagem de erro
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

                //Define que as casas das Sheep e do Wolf nao sao jogáveis
                board[sheep1Pos[0], sheep1Pos[1]].isPlayable = false;
                board[sheep2Pos[0], sheep2Pos[1]].isPlayable = false;
                board[sheep3Pos[0], sheep3Pos[1]].isPlayable = false;
                board[sheep4Pos[0], sheep4Pos[1]].isPlayable = false;
                board[wolfPos[0], wolfPos[1]].isPlayable = false;

                    
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
                        wolfPos[1] = Convert.ToInt16(aux2);

                        // Se meter o número valido, sai do loop
                        if (board[wolfPos[0], wolfPos[1]].isPlayable)
                        {
                            break;
                        }

                        // Se não for válido vai repetir o processo
                        Console.WriteLine(" ");
                        Console.WriteLine("Please choose a valid number");
                    } while(board[wolfPos[0], wolfPos[1]].isPlayable == false);
                }
                    

                // Jogadas para o WOLF
                // Quando o numero de jogadas é ímpar
                else if (numberOfPlays % 2 != 0)
                {
                    do 
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine("----------- WOLF TURN -----------");
                        if (WolfEndGame())
                        {
                            
                            victory("Wolf", numberOfPlays);
                            gameOver = true;
                            break;
                            

                        }
                        // Pedir input ao jogador
                        Console.Write("Insert a row number: ");
                        aux1 = Console.ReadLine();
                        Console.Write("Insert a column number: ");
                        aux2 = Console.ReadLine();
                        // Se escrever exit, sai do jogo
                        if (aux1 == "exit" || aux2 == "exit")
                        {
                            gameOver = true;
                            break;
                        }
                        
                        try
                        {
                            wolfTempPos[0] = Convert.ToInt16(aux1);
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
                            Console.WriteLine("The input must be a valid line" +
                            " number or the 'exit' command.");
                            continue;
                        }

                        try
                        {
                            wolfTempPos[1] = Convert.ToInt16(aux2);
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
                            Console.WriteLine("The input must be a valid line" +
                            " number or the 'exit' command.");
                            continue;
                        }
                        
                        // Se meter o número valido, sai do loop
                        // Só aceita números com +1 ou -1 que a casa atual
                        if (isInputInBoard())
                            if (isInputDifferentHouse())
                                if (isInputLegalForward())
                                    if (isInputLegalBackwards())
                                    {

                                        // Transforma as próximas cordernadas 
                                        // nas do input do utilizador
                                        if (board[wolfTempPos[0], wolfTempPos[1]].
                                                isPlayable)
                                        {
                                            // Define posição em que o WOLF 
                                            // estava como playable = TRUE
                                            board[wolfPos[0], wolfPos[1]].
                                                    isPlayable = true;
                                            // Define nova posição do lobo e
                                            // define como playable = False
                                            wolfPos[0] = wolfTempPos[0];
                                            wolfPos[1] = wolfTempPos[1];
                                            board[wolfPos[0], wolfPos[1]].
                                                isPlayable = false;
                                            break; 
                                        }
                                    }
                        
                        // Imprime que casas podem ser jogadas
                        Console.WriteLine("");
                        if (wolfPos[0] > 0)
                            Console.WriteLine($"*Possible Vertical Move:"+
                                    $" Row {wolfPos[0]-1}*");
                        if (wolfPos[0] < 7)
                            Console.WriteLine($"*Possible Vertical Move:"+
                                    $" Row {wolfPos[0]+1}*");
                        if (wolfPos[1] > 0)
                            Console.WriteLine($"*Possible Horizontal Move:"+
                                    $" Column {wolfPos[1]-1}*");
                        if (wolfPos[1] < 7)
                            Console.WriteLine($"*Possible Horizontal Move:"+
                                    $" Column {wolfPos[1]+1}*");
  
                        // Se não for válido vai repetir o loop
                        Console.WriteLine("");
                        Console.WriteLine("Please choose a valid number");
                    } while(board[wolfPos[0], wolfPos[1]].isPlayable == false);
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
                    
                    do
                    {
                        aux3 = Console.ReadLine().ToUpper();
                        Console.Write($"You chose {aux3}\n");
                        sheepChosen(aux3);


                       /* switch (aux3))
                       {
                            case "S1":
                                sheepNewPos[0] = sheep1Pos[0];
                                sheepNewPos[1] = sheep1Pos[1];
                            break;
                            case "S2":
                                sheepNewPos[0] = sheep2Pos[0];
                                sheepNewPos[1] = sheep2Pos[1];
                            break;
                            case "S3":
                                sheepNewPos[0] = sheep3Pos[0];
                                sheepNewPos[1] = sheep3Pos[1];
                            break;
                            case "S4":
                                sheepNewPos[0] = sheep4Pos[0];
                                sheepNewPos[1] = sheep4Pos[1];
                                break;
                            default:
                                Console.WriteLine("Error");
                                break; 

                       }*/
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
                            sheepTempPos[0] = Convert.ToInt16(aux1);
                            sheepTempPos[1] = Convert.ToInt16(aux2);

                            // Se meter o número valido, sai do loop
                            // Só aceita números com +1 ou -1 na Horizontal
                            // e -1 na Vertical que a casa atual
                            if (sheepTempPos[0] >= 0 && sheepTempPos[0] <= 8 && 
                                sheepTempPos[1] >= 0 && sheepTempPos[1] <= 8 )                    
                                if (sheepTempPos[0] != sheepNewPos[0] && 
                                    sheepTempPos[1] != sheepNewPos[1] )
                                    if ( sheepTempPos[0] < sheepNewPos[0] && 
                                        sheepTempPos[1] < sheepNewPos[1] + 2)
                                        if (sheepTempPos[0] > sheepNewPos[0] - 2 && 
                                            sheepTempPos[1] > sheepNewPos[1] -2)      
                                        {

                                            // Transforma as próximas cordernadas 
                                            // nas do input do utilizador
                                            if (board[sheepTempPos[0], sheepTempPos[1]].
                                                    isPlayable)
                                            {
                                                // Define posição em que a Sheep
                                                // estava como playable = TRUE
                                                board[sheepNewPos[0], sheepNewPos[1]].
                                                        isPlayable = true;
                                                // Define nova posição da sheep e
                                                // define como playable = False
                                                sheepNewPos[1] = sheepTempPos[1];
                                                sheepNewPos[0] = sheepTempPos[0];
                                                board[sheepNewPos[0], sheepNewPos[1]].
                                                    isPlayable = false;
                                                break;
                                            }
                                        }
                                        Console.WriteLine("Please choose a" +
                                                " valid number");
                    } while(board[sheepNewPos[0], sheepNewPos[1]].isPlayable == false);
                    
                    switch (aux3)
                    {
                        case "S1":
                            sheep1Pos[0] = sheepNewPos[0];
                            sheep1Pos[1] = sheepNewPos[1];
                        break;
                        case "S2":
                            sheep2Pos[0] = sheepNewPos[0];
                            sheep2Pos[1] = sheepNewPos[1];
                        break;
                        case "S3":
                            sheep3Pos[0] = sheepNewPos[0];
                            sheep3Pos[1] = sheepNewPos[1];
                        break;
                        case "S4":
                            sheep4Pos[0] = sheepNewPos[0];
                            sheep4Pos[1] = sheepNewPos[1];
                        break;
                        default:
                            break;
                    }
                }
                                           
                // Posições dos animais
                board[wolfPos[0], wolfPos[1]].animal = "Wolf";
                board[sheep1Pos[0], sheep1Pos[1]].animal = "Sheep_01";
                board[sheep2Pos[0], sheep2Pos[1]].animal = "Sheep_02";
                board[sheep3Pos[0], sheep3Pos[1]].animal = "Sheep_03";
                board[sheep4Pos[0], sheep4Pos[1]].animal = "Sheep_04";
                
                // Imprime o tabuleiro após cada jogada
                printBoard(board);

                // Incrementa número de jogadas após cada jogada
                numberOfPlays++;

                // Chama função de Victory
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
            Console.WriteLine($"Number of total plays = {plays}");
        }

        private static bool isInputInBoard()
        {   
            return wolfTempPos[0] >= 0 && wolfTempPos[0] <= 8 && 
                wolfTempPos[1] >= 0 && wolfTempPos[1] <= 8;
        }
        
        private static bool isInputDifferentHouse()
        {
            return wolfTempPos[0] != wolfPos[0] && wolfTempPos[1] != wolfPos[1]; 
        }
        
        private static bool isInputLegalForward()
        {
            return wolfTempPos[0] < wolfPos[0] + 2 && 
                wolfTempPos[1] < wolfPos[1] + 2;
        }

        private static bool isInputLegalBackwards()
        {
            return wolfTempPos[0] > wolfPos[0] - 2 && 
                wolfTempPos[1] > wolfPos[1] -2;
        }
        static void printBoard(Square[,] board)
        {
                // Imprime o tabuleiro
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Numbers = Playable Squares");
                Console.WriteLine("White Squares = Unplayable Squares");
                Console.WriteLine("exit  <- to quit the game");

                // Imprime números no topo do tabuleiro
                Console.WriteLine("");
                Console.WriteLine("      0     1     2     3     4     5" +
                    "     6     7");
                Console.WriteLine("");
                for (int i = 0; i < 8; i++)
                {
                    // Variável para imprimir cada linha 3 vezes
                    int boardTemp = 0; 
                    
                    while (boardTemp <3)
                    {
                        // So na linha do meio de cada quadrado
                        // Imprime os números à volta do tabuleiro
                        if (boardTemp == 1)
                            Console.Write($"{i}   ");
                        else
                            Console.Write($"    ");

                        for (int j = 0; j < 8; j++)
                        {
                            
                                // Imprime o WOLF em vez dos números
                                if (board[i,j].animal == "Wolf")
                                    if (boardTemp == 1)
                                        Console.Write("  WW  ");
                                    else
                                    Console.Write("      ");

                                // Imprime a SHEEP em vez dos números
                                // Imprime SHEEP em sinal de Mais
                                else if (board[i,j].animal == "Sheep_01")
                                {
                                    if (boardTemp == 1)
                                        Console.Write("  S1  ");
                                    else
                                    Console.Write("      ");
                                }

                                else if (board[i,j].animal == "Sheep_02")
                                {
                                    if (boardTemp == 1)
                                        Console.Write("  S2  ");
                                    else
                                    Console.Write("      ");
                                }

                                else if (board[i,j].animal == "Sheep_03")
                                {
                                    if (boardTemp == 1)
                                        Console.Write("  S3  ");
                                    else
                                    Console.Write("      ");
                                }

                                else if (board[i,j].animal == "Sheep_04")
                                {
                                    if (boardTemp == 1)
                                        Console.Write("  S4  ");
                                    else
                                    Console.Write("      ");
                                }
                                    
                                // Se der para jogar imprime a linha e a coluna
                                else if (board[i,j].isPlayable)
                                {
                                    if (boardTemp == 1)
                                        Console.Write($"  {i}{j}  ");
                                    else
                                        Console.Write($"      ");
                                }

                                // Se não der para jogar imprime a branco
                                else if (board[i,j].isPlayable == false)
                                    Console.Write($"||||||");

                            if (j==7)
                            {
                                // So na linha do meio de cada quadrado
                                // Imprime os números no fim do tabuleiro
                                if (boardTemp == 1)
                                    Console.Write($"   {i}");

                                // Incrementa a variável para sair do while
                                boardTemp++;                            
                                Console.WriteLine("");
                            }
                        }
                    }
                }
                // Imprime números em baixo do tabuleiro
                Console.WriteLine("\n      0     1     2     3     4     5" +
                    "     6     7");
        }
    
        private static bool WolfEndGame()
        {
            //If Wold is on the left side
            if (wolfPos[1] == 0)
            {
                return board[wolfPos[0]+1,wolfPos[1]+1].isPlayable == false && 
                board[wolfPos[0]-1,wolfPos[1]+1].isPlayable == false;
            }
            if (wolfPos[1] == 7)
            {
                return board[wolfPos[0]+1,wolfPos[1]-1].isPlayable == false && 
                board[wolfPos[0]-1,wolfPos[1]-1].isPlayable == false;
            }
            if  (wolfPos[0] == 0)
            {
                return board[wolfPos[1]+1,wolfPos[0]+1].isPlayable == false && 
                board[wolfPos[1]-1,wolfPos[0]+1].isPlayable == false;
            }
            if  (wolfPos[0] == 7)
            {
                return board[wolfPos[1]+1,wolfPos[0]-1].isPlayable == false && 
                board[wolfPos[1]-1,wolfPos[0]-1].isPlayable == false;
            }
            else
            {
                return board[wolfPos[0]+1,wolfPos[1]+1].isPlayable == false &&
                board[wolfPos[0]-1,wolfPos[1]-1].isPlayable == false &&
                board[wolfPos[0]+1,wolfPos[1]-1].isPlayable == false && 
                board[wolfPos[0]-1,wolfPos[1]+1].isPlayable == false;        
            }    
        }

        private static (int, int) sheepChosen(string auxTemp)
        {
            switch (auxTemp)
            {
                case "S1":
                    sheepNewPos[0] = sheep1Pos[0];
                    sheepNewPos[1] = sheep1Pos[1];
                break;
                case "S2":
                    sheepNewPos[0] = sheep2Pos[0];
                    sheepNewPos[1] = sheep2Pos[1];
                break;
                case "S3":
                    sheepNewPos[0] = sheep3Pos[0];
                    sheepNewPos[1] = sheep3Pos[1];
                break;
                case "S4":
                    sheepNewPos[0] = sheep4Pos[0];
                    sheepNewPos[1] = sheep4Pos[1];
                    break;
                default:
                    sheepChosen(auxTemp);
                    break;
            }
            return (sheepNewPos[0],sheepNewPos[1]);
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