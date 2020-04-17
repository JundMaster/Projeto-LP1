using System.Globalization;
using System.IO;
using System.Dynamic;
using System.Collections;
using System.Linq;
using System;
using System.Threading;

namespace WolfAndSheep
{
    class Program
    {
        // Declaração de variáveis
        // Cria uma array bidimensional de Squares
        static Square[,] board;

        // Variável para o input do jogador para o movimento do WOLF
        static int [] wolfNewPos = new int[2] {0, 0};

        // Variáveis para a posição Inicial do WOLF
        static int [] wolfPos = new int[2] {0, 0};

        // Posições de vitória para o WOLF
        static int[] wolfVictoryPositions = new int[4] {0, 2, 4, 6};

        // Variáveis para o input do jogador para o movimento da Sheep
        static int [] sheepTempPos = new int[2] {0, 0};
        static int [] sheepNewPos = new int[2] {0, 0};

        // Variáveis para a posição Inicial das Sheep
        static int [] sheep1Pos = new int[2] {7, 0};
        static int [] sheep2Pos = new int[2] {7, 2};
        static int [] sheep3Pos = new int[2] {7, 4};
        static int [] sheep4Pos = new int[2] {7, 6};
        // Mensagem de erro
        public static string errorMessage = "\n --------------------- INVALID "+ "INPUT ---------------------\n";
        
        public static string auxTemp = "";
        
        public static string validNumMessage = " Please, insert a" 
        +" valid number!";
        public static string initialNum = " --  1         3         5"+"         7  --";

        // Varíaveis temporárias para o input do jogador
        static string aux1, aux2, aux3;

        // Variável para saber quando o jogo está a correr
        static bool gameOver = false;

        // Variável para o número de jogadas
        static int numberOfPlays = 0;

        /// <summary>
        /// Método main chama as funções para correr o jogo
        /// </summary>
        static void Main(string[] args)
        {
            Intro();
            Game();
            Console.WriteLine("\n Bye Bye");
        }

        /// <summary>
        /// Função que controla o ciclo no qual consiste o jogo
        /// </summary>
        private static void Game()
        {
            //Variável que define o tamanho do tabuleiro de jogo
            board = new Square[8, 8]; 

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
                    // Resto dos quadrados (é preto e playable)
                    else
                        board[i,j].isPlayable = true;
                }
            }

            while (gameOver == false)
            {
                //Define que as casas das Sheep e do Wolf nao sao jogáveis
                board[sheep1Pos[0], sheep1Pos[1]].isPlayable = false;
                board[sheep2Pos[0], sheep2Pos[1]].isPlayable = false;
                board[sheep3Pos[0], sheep3Pos[1]].isPlayable = false;
                board[sheep4Pos[0], sheep4Pos[1]].isPlayable = false;
                board[wolfPos[0], wolfPos[1]].isPlayable = false;

                    
                // --- GAMEPLAY
                // Apenas para a primeira jogada no jogo
                if (numberOfPlays == 0)
                {
                    FirstTurn();
                }
                    
                // Jogadas para o WOLF
                // Quando o numero de jogadas é ímpar
                else if (numberOfPlays % 2 != 0)
                {
                    WolfTurn(board);
                }
                
                // Jogadas para A SHEEP
                // Quando o numero de jogadas é par
                else if (numberOfPlays % 2 == 0)
                {   
                    Console.WriteLine("");
                    Console.WriteLine(" -------------------------------------" +
                                        " FLOCK TURN --------");
                    Console.WriteLine("\n Choose a Sheep to play:\n");
                    Console.Write(" -- S1     S2     S3    S4 --  : ");
                    aux3 = Console.ReadLine().ToUpper();
                    SheepChosen(aux3);
                    if (auxTemp == "invalid")
                        {
                            PrintBoard(board);
                            Console.WriteLine(errorMessage);
                            Console.WriteLine("That is not a valid"+
                            " sheep choice.");
                            continue;
                        }
                    else if(auxTemp == "blocked")
                    {
                        PrintBoard(board);
                        Console.WriteLine(errorMessage);
                        Console.WriteLine("That sheep is blocked."+
                        "Pick another one.");
                        continue;   
                    }
                    
                    SheepTurn(board);
                }
                                            
                // Posições dos animais
                board[wolfPos[0], wolfPos[1]].animal = "Wolf";
                board[sheep1Pos[0], sheep1Pos[1]].animal = "Sheep_01";
                board[sheep2Pos[0], sheep2Pos[1]].animal = "Sheep_02";
                board[sheep3Pos[0], sheep3Pos[1]].animal = "Sheep_03";
                board[sheep4Pos[0], sheep4Pos[1]].animal = "Sheep_04";
                
                // Imprime o tabuleiro após cada jogada
                if (gameOver == false)
                    PrintBoard(board);

                // Chama função de Victory
                // Vai detetar se o Wolf está num dos últimos Squares
                foreach (int x in wolfVictoryPositions)
                {
                    if (board[7,x].animal == "Wolf")
                    {
                        Victory("Wolf", numberOfPlays);
                        gameOver = true;
                    }
                }

                // Incrementa número de jogadas após cada jogada
                numberOfPlays++;
            }
        }

/*-------------------------------- Métodos -----------------------------------*/

        /// <summary>
        /// Função que imprime as regras ddo jogo e as possíveis casas iniciais 
        /// do Lobo
        /// </summary>
        private static void Intro()
        {
            Console.WriteLine("");
            Console.WriteLine(@"             ___        ___               " +
            " __        ___  ___  ___  ");
            Console.WriteLine(@" \        / |   | |    |      _      _    " +
            "|  ' |  | |    |    |   | ");
            Console.WriteLine(@"  \  /\  /  |   | |    |--   |_||\ || \   " +
            "|__  |--| |--  |--  |---  ");
            Console.WriteLine(@"   \/  \/   |___| |___ |     | || \||_/   " +
            ".__| |  | |___ |___ |     ");

            Console.WriteLine("");
            Console.WriteLine("");
            Thread.Sleep(2000);
            
            Thread.Sleep(200);
            Console.WriteLine(" INSTRUCTIONS:");
            Console.WriteLine(" The wolf (WW) must reach the bottom squares" +
                " to win.");
            Thread.Sleep(200);
            Console.WriteLine(" The flock of sheep (S1, S2, S3, S4) win the" +
                " game if they block wolf's path.");
            Thread.Sleep(200);
            Console.WriteLine("\n Both animals can only move" +
                @" ONE diagonal square per turn.");
            Thread.Sleep(200);
            Console.WriteLine(" The flock of sheep (S1, S2, S3, S4) can only" +
                " move top.");
            Thread.Sleep(200);
            Console.WriteLine(" The wolf (WW) can move top and bottom");
            Thread.Sleep(200);
            Console.WriteLine(" The flock of sheep can ONLY move one sheep" +
                " per turn.");
            Thread.Sleep(200);
            Console.WriteLine(" Both animals can only move to numbered/empty" + 
                " squares, they can't move to occupied squares.");
            Thread.Sleep(200);
            Console.WriteLine("\n Players must type the board's row and" +
                " column to move the animal.");
            Thread.Sleep(200);

            Console.WriteLine("\n -- Insert Wolf's Initial Square Number--");
            Thread.Sleep(200);
            Console.WriteLine(initialNum);
            Thread.Sleep(200);
        }

        /// <summary>
        /// Função que imprime a mensagem de vitória com o animal vencedor e o
        /// número de jogadas feitas.
        /// </summary>
        /// <param name="animal">Variável que guarda o tipo de animal 
        /// vencedor</param>
        /// <param name="plays"> Variável que guarda o número de jogadas 
        /// feitas</param>
        private static void Victory(string animal, int plays)
        {
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine($" Congratulations, {animal} won the game !!!");
            Console.WriteLine($" Number of total plays = {plays}");
        }
        
        /// <summary>
        /// Função que verifica se o Input do jogar é legal tendo em conta as
        /// regras do jogo
        /// </summary>
        /// <param name="animal"> Variável que guarda o tipo de animal está a
        ///  jogar no turno atual</param>
        /// <returns>Retorna "True" se a jogada for válida ou "Falso" se for
        /// inválida</returns>
        private static bool LegalMove(string animal)
        {
            bool canMove;
            if (animal == "wolf")
                // Verifica que tá a 1 quadrado de distância da posição atual
                if (wolfNewPos[0] >= 0 && wolfNewPos[0] < 8 && 
                    wolfNewPos[1] >= 0 && wolfNewPos[1] < 8 &&
                    wolfNewPos[0] != wolfPos[0] && 
                    wolfNewPos[1] != wolfPos[1] &&
                    wolfNewPos[0] < wolfPos[0] + 2 && 
                    wolfNewPos[1] < wolfPos[1] + 2 &&
                    wolfNewPos[0] > wolfPos[0] - 2 && 
                    wolfNewPos[1] > wolfPos[1] - 2)
                    canMove = true;
                else
                    canMove = false;
            else
                // Verifica que tá a 1 quadrado de distância da posição atual
                // e que a row é MENOR (para andar para cima)
                if (sheepTempPos[0] >= 0 && sheepTempPos[0] < 8 && 
                    sheepTempPos[1] >= 0 && sheepTempPos[1] < 8 &&
                    sheepTempPos[0] != sheepNewPos[0] && 
                    sheepTempPos[1] != sheepNewPos[1] &&
                    sheepTempPos[0] < sheepNewPos[0] &&
                    sheepTempPos[1] < sheepNewPos[1] + 2 &&
                    sheepTempPos[0] > sheepNewPos[0] - 2 && 
                    sheepTempPos[1] > sheepNewPos[1] - 2)
                    canMove = true;
                else
                    canMove = false;
            return canMove;
        }


        /// <summary>
        /// Função que imprime todo o tabuleiro
        /// </summary>
        /// <param name="board">Variável que guarda os valores para as posições
        /// de cada casa do tabuleiro</param>
        private static void PrintBoard(Square[,] board)
        {
                // Imprime o tabuleiro
                Console.WriteLine(" --------------------------------------" +
                    "-------------------");
                Console.WriteLine(" WW  -> Wolf");
                Console.WriteLine(" S1, S2, S3, S4  -> Sheep");
                Console.WriteLine(" Numbers  -> Playable Squares");
                Console.WriteLine(" White Squares  -> Unplayable Squares");
                Console.WriteLine(" Type ' exit ' to quit the game");

                // Imprime números no topo do tabuleiro
                Console.WriteLine("");
                Console.WriteLine("       0     1     2     3     4     5" +
                    "     6     7");
                Console.WriteLine("    " + 
                String.Concat(Enumerable.Repeat("__ ", 17)));
                Console.WriteLine("");
                for (int i = 0; i < 8; i++)
                {
                    // Variável para imprimir cada linha 3 vezes
                    int boardTemp = 0; 
                    
                    while (boardTemp <3)
                    {
                        // Imprime o número à esquerda do tabuleiro
                        // só na linha do meio de cada quadrado
                        if (boardTemp == 1)
                            Console.Write($" {i} | ");
                        else
                            Console.Write("   | ");

                        for (int j = 0; j < 8; j++)
                        {
                                // Imprime WW na posição do wolf
                                // no meio do quadrado
                                if (board[i,j].animal == "Wolf")
                                    if (boardTemp == 1)
                                        Console.Write(" (WW) ");
                                    else
                                        Console.Write("      ");

                                // Imprime SX na posição da sheep
                                // no meio do quadrado
                                else if (board[i,j].animal == "Sheep_01")
                                {
                                    if (boardTemp == 1)
                                        Console.Write(" <S1> ");
                                    else
                                        Console.Write("      ");
                                }

                                else if (board[i,j].animal == "Sheep_02")
                                {
                                    if (boardTemp == 1)
                                        Console.Write(" <S2> ");
                                    else
                                        Console.Write("      ");
                                }

                                else if (board[i,j].animal == "Sheep_03")
                                {
                                    if (boardTemp == 1)
                                        Console.Write(" <S3> ");
                                    else
                                        Console.Write("      ");
                                }

                                else if (board[i,j].animal == "Sheep_04")
                                {
                                    if (boardTemp == 1)
                                        Console.Write(" <S4> ");
                                    else
                                        Console.Write("      ");
                                }
                                    
                                // Se der para jogar imprime a linha e a coluna
                                // no meio do quadrado
                                else if (board[i,j].isPlayable)
                                {
                                    if (boardTemp == 1)
                                        Console.Write($"  {i}{j}  ");
                                    else
                                        Console.Write("      ");
                                }

                                // Se não der para jogar imprime a branco
                                else if (board[i,j].isPlayable == false)
                                    Console.Write("######");

                            if (j==7)
                            {
                                // Imprime os números no fim do tabuleiro
                                if (boardTemp == 1)
                                    Console.Write($" | {i}");
                                else
                                    Console.Write(" |    ");

                                Console.WriteLine("");
                                // Incrementa a variável para sair do while
                                boardTemp++;                            
                            }
                        }
                    }
                }
                // Imprime números em baixo do tabuleiro
                Console.WriteLine("    " + 
                String.Concat(Enumerable.Repeat("__ ", 17)));
                Console.WriteLine("\n       0     1     2     3     4     5" +
                    "     6     7");
                
                    
        }
    
        /// <summary>
        /// Função que verifica se o Lobo tem jogadas possíveis quando esta nas
        /// bordas do tabuleiro ou no meio
        /// </summary>
        /// <returns> Retorna "True" se o Lobo não tiver jogadas
        /// possíveis e "Falso" se ainda existirem jogadas. </returns>
        private static bool WolfGameOver()
        {
            bool gameOver;
            // Se o Wolf estiver no quadrado da esquerda
            if (wolfPos[1] == 0)
            {
                gameOver = board[wolfPos[0]+1,wolfPos[1]+1].isPlayable == false && 
                board[wolfPos[0]-1,wolfPos[1]+1].isPlayable == false;
            }
            // Se o Wolf estiver no quadrado da direita
            else if (wolfPos[1] == 7)
            {
                gameOver = board[wolfPos[0]+1,wolfPos[1]-1].isPlayable == false && 
                board[wolfPos[0]-1,wolfPos[1]-1].isPlayable == false;
            }
            // Se o Wolf estiver no quadrado de cima
            else if  (wolfPos[0] == 0)
            {
                gameOver = board[wolfPos[1]+1,wolfPos[0]+1].isPlayable == false && 
                board[wolfPos[1]-1,wolfPos[0]+1].isPlayable == false;
            }
            // Se o Wolf estiver no quadrado de baixo
            else if  (wolfPos[0] == 7)
            {
                gameOver = board[wolfPos[1]+1,wolfPos[0]-1].isPlayable == false && 
                board[wolfPos[1]-1,wolfPos[0]-1].isPlayable == false;
            }
            // Se o Wolf estiver em outros pontos do tabuleiro
            else
            {
            
                gameOver = board[wolfPos[0]+1,wolfPos[1]+1].isPlayable == false &&
                board[wolfPos[0]-1,wolfPos[1]-1].isPlayable == false &&
                board[wolfPos[0]+1,wolfPos[1]-1].isPlayable == false && 
                board[wolfPos[0]-1,wolfPos[1]+1].isPlayable == false;        
            } 
            return gameOver;   
        }
        
        /// <summary>
        /// Função que verifica se a Ovelha escolhida tem jogadas possíveis ou 
        /// caso todas as ovelhas estejam bloqueadas fecha o jogo dando a 
        /// vitória ao Lobo
        /// </summary>
        /// <param name="aux3"> Variável que guarda o nome da Ovelha 
        /// escolhida</param>
        /// <returns> Retorna "True" se a Ovelha escolhida não tiver jogadas
        /// possíveis e "Falso" se ainda existirem jogadas. </returns>
        private static bool SheepGameOver(string aux3)
        {
            bool gameOver;
            if (aux3 == "S1")
            {
                sheepNewPos[0] = sheep1Pos[0];
                sheepNewPos[1] = sheep1Pos[1];
                
                if (sheepNewPos[1] == 0)
                {   
                    gameOver = board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;
                }
                else if (sheepNewPos[0] == 0)
                    gameOver = true;
                    
                else
                {
                    gameOver = board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false &&
                    board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;        
                }
            }
            
            else if (aux3 == "S2")
            {
                sheepNewPos[0] = sheep2Pos[0];
                sheepNewPos[1] = sheep2Pos[1];
                
                if (sheepNewPos[1] == 0)
                {
                    gameOver = board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;
                }
                if (sheepNewPos[0] == 0)
                    gameOver = true;

                else
                {
                    gameOver = board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false &&
                    board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;        
                }
                
            }

            else if (aux3 == "S3")
            {
                sheepNewPos[0] = sheep3Pos[0];  
                sheepNewPos[1] = sheep3Pos[1];
                
                
                if (sheepNewPos[1] == 0)
                {
                    gameOver = board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;
                }
                if (sheepNewPos[0] == 0)
                    gameOver = true;

                else
                {
                    
                    gameOver = board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false &&
                    board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;        
                }
            }

            else if (aux3 == "S4")
            {
                sheepNewPos[0] = sheep4Pos[0];
                sheepNewPos[1] = sheep4Pos[1];

               
                if (sheepNewPos[1] == 0)
                {
                    gameOver = board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;
                }
                if (sheepNewPos[0] == 0)
                    gameOver = true;

                else
                {
                    
                    gameOver = board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false &&
                    board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;        
                }
            }
            else
            {
                gameOver = false;
            }
            return gameOver; 
        }
        
        /// <summary>
        /// Função que controla o Input do jogador para a seleção da 
        /// ovelha a jogar
        /// </summary>
        /// <returns>Retorna os valores para a posição da Ovelha escolhida
        /// e o seu nome</returns>
        private static (int, int, string) SheepChosen(string aux3)
        {
            int [][] sheepList = new int [4][] {sheep1Pos, sheep2Pos,
            sheep3Pos, sheep4Pos};

            string [] sheepString = new string [4] {"S1", "S2", "S3", "S4"};

            for (int i = 0; i < 4; i++)
            {
                if (aux3 == sheepString[i])
                {
                    sheepNewPos[0] = sheepList[i][0];
                    sheepNewPos[1] = sheepList[i][1];
                    auxTemp = sheepString[i];
                    if(SheepGameOver(aux3))
                        auxTemp = "blocked";
                    break;
                }
                else
                    auxTemp = "invalid";
                
            }
            return (sheepNewPos[0],sheepNewPos[1], auxTemp);
        }
        
        /// <summary>
        /// Função que verifica se o Input do jogador é válido
        /// </summary>
        /// <param name="aux1"> Variável que guarda o Input do Jogador após 
        /// conversão para inteiro</param>
        /// <param name="aux2"> Variável que guarda O Input do jogador em 
        /// formato string para conversão</param>
        /// <param name="numberOfPlays"> Número de jogadas até ao momento</param>
        /// <param name="board"> Array que contém o tabuleiro de jogo</param>
        /// <returns>Retorna "True" caso o Input seja válido ou "Falso" caso 
        /// seja inválido</returns>
        private static bool CheckConvert(int aux1, string aux2, int numberOfPlays,
            Square[,] board)
        {
            Console.WriteLine("");
            bool canConvert; 
            try
            {
                aux1 = Convert.ToInt16(aux2);
                canConvert = true;
            }
            catch (OverflowException)
            {
                if (numberOfPlays != 0)
                    PrintBoard(board);
                Console.WriteLine(errorMessage);
                Console.WriteLine(validNumMessage);
                canConvert = false;
                if (numberOfPlays == 0)
                    Console.WriteLine(initialNum);

            }
            catch (FormatException)
            {
                if (numberOfPlays != 0)
                    PrintBoard(board);
                Console.WriteLine(errorMessage);
                Console.WriteLine(" Please, insert a number, not a string!");
                canConvert = false;
                if (numberOfPlays == 0)
                    Console.WriteLine(initialNum);
            }
            return canConvert;
        }
    
        /// <summary>
        /// Função que controla o Input para a casa Inicial do Lobo
        /// </summary>
        private static void FirstTurn()
        {
            // Faz o loop do input do jogador ate colocar um 
            // Square válido
            do 
            {
                Console.WriteLine("");
                
                // Pedir input ao jogador
                Console.Write(" Insert a starting position: ");
                aux2 = Console.ReadLine().ToLower();

                // Se escrever exit, sai do jogo
                if (aux2 == "exit")
                {
                    gameOver = true;
                    break;
                }

                //Converte a string do input para int
                if (CheckConvert(wolfPos[1], aux2, numberOfPlays, board))
                   { 
                        wolfPos[1] = Convert.ToInt16(aux2);
                        if(wolfPos[1] >= 8 || wolfPos[1] <= 0)
                        {
                            Console.WriteLine(errorMessage);
                            Console.Write(validNumMessage);
                            Console.WriteLine(initialNum);
                            wolfPos[1] = 0;
                            continue;
                        }     
                    }
                else
                    continue;
                

                // Se meter o número valido, sai do loop
                if (board[wolfPos[0], wolfPos[1]].isPlayable)
                {
                    break;
                }

                Console.WriteLine(errorMessage);
                // Se não for válido vai repetir o processo
                Console.WriteLine(validNumMessage);
                Console.WriteLine($"\n{initialNum}");
            } while(board[wolfPos[0], wolfPos[1]].isPlayable == false);
        }
    
        /// <summary>
        /// Função que controla todo o Input do Turno do Lobo
        /// </summary>
        /// <param name="board"> Array que contém o tabuleiro de jogo</param>
        private static void WolfTurn(Square[,] board)
        {
            do 
            {
                Console.WriteLine("");
                Console.WriteLine(" --------- WOLF TURN --------------" +
                                "-----------------------");
                
                // Verifica se o Wolf tem jogadas possíveis
                // Caso não tenha, o jogo acaba
                if (WolfGameOver())
                {
                    // Imprime a mensage de Vitoria
                    // E o Numero de Jogadas feitas 
                    Victory("Sheep", numberOfPlays);
                    
                    gameOver = true;
                    break;
                }

                Console.Write("\n Insert a row number: ");
                aux1 = Console.ReadLine().ToLower();

                Console.Write(" Insert a column number: ");
                aux2 = Console.ReadLine().ToLower();

                // Se escrever exit, sai do jogo
                if (aux1 == "exit" || aux2 == "exit")
                {
                    gameOver = true;
                    break;
                }

                if (CheckConvert(wolfNewPos[0], aux1, numberOfPlays, board))
                    wolfNewPos[0] = Convert.ToInt16(aux1);
                else
                    continue;

                if (CheckConvert(wolfNewPos[1], aux2, numberOfPlays, board))
                    wolfNewPos[1] = Convert.ToInt16(aux2);
                else
                    continue;
                
                // Se meter o número valido, movimenta o lobo
                // Só aceita números com +1 ou -1 que a casa atual
                if (LegalMove("wolf"))
                {
                    // Verifica se a posição do input é Playable
                    if (board[wolfNewPos[0], wolfNewPos[1]].isPlayable)
                    {
                        // Define posição em que o WOLF 
                        // estava como playable = TRUE
                        board[wolfPos[0], wolfPos[1]].isPlayable = true;
                        board[wolfPos[0], wolfPos[1]].animal = "none";

                        // Define nova posição do lobo e
                        // define como playable = False
                        wolfPos[0] = wolfNewPos[0];
                        wolfPos[1] = wolfNewPos[1];
                        board[wolfPos[0], wolfPos[1]].isPlayable = false;
                        break; 
                    }
                }

                PrintBoard(board);
                
                Console.WriteLine(errorMessage);
                // Se o Input não for válido vai repetir o loop
                Console.WriteLine(validNumMessage);
            } while(board[wolfPos[0], wolfPos[1]].isPlayable == false);
        }
    
        /// <summary>
        /// Função que controla todo o Input do Turno das Ovelhas
        /// </summary>
        /// <param name="board"> Array que contém o tabuleiro de jogo</param>
        private static void SheepTurn(Square[,] board)
        {
            do
            {
                Console.Write($"\n You chose {aux3}\n");
                // Pedir input ao jogador
                Console.Write("\n Insert a row number: ");
                aux1 = Console.ReadLine().ToLower();
                Console.Write(" Insert a column num: ");
                aux2 = Console.ReadLine().ToLower();

                // Se escrever exit, sai do jogo
                if (aux1 == "exit" || aux2 == "exit")
                {
                    gameOver = true;
                    break;
                }
                
                // Tentar converter o input para int
                // Se não conseguir imprime mensagens de erro
                if (CheckConvert(sheepTempPos[0], aux1, numberOfPlays, board))
                    sheepTempPos[0] = Convert.ToInt16(aux1);
                else
                    continue;

                if (CheckConvert(sheepTempPos[1], aux2, numberOfPlays, board))
                    sheepTempPos[1] = Convert.ToInt16(aux2);
                else
                    continue;
            
                
                // Se meter o número valido, movimenta o lobo
                // Só aceita números com +1 ou -1 que a casa atual 
                if (LegalMove("sheep"))     
                {
                    // Verifica se a posição do input é Playable
                    if (board[sheepTempPos[0], sheepTempPos[1]].
                            isPlayable)
                    {
                        // Define posição em que a Sheep
                        // estava como playable = TRUE
                        board[sheepNewPos[0], sheepNewPos[1]].isPlayable = true;
                        board[sheepNewPos[0], sheepNewPos[1]].animal = "none";
                        // Define nova posição da sheep e
                        // define como playable = False
                        sheepNewPos[1] = sheepTempPos[1];
                        sheepNewPos[0] = sheepTempPos[0];
                        board[sheepNewPos[0], sheepNewPos[1]].
                            isPlayable = false;
                        break;
                    }
                }

                PrintBoard(board);

                Console.WriteLine(errorMessage);
                Console.WriteLine(validNumMessage);
            } while(board[sheepNewPos[0], sheepNewPos[1]].isPlayable == false);
                
            // Depois da jogada vai dar o valor da nova posição
            // à Sheep que foi escolhida
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
    }


/*----------------------------------CLASSES-----------------------------------*/

    // Definição da Classe que constitui as casas do tabuleiro
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