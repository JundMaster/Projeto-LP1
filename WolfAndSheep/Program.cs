using System.Globalization;
using System.IO;
using System.Dynamic;
using System.Collections;
using System.Linq;
using System;

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

        //Variavel que verifica se a ovelha já se moveu
        static bool hasMoved = false;

        public static string auxTemp = "";

        // Varíaveis temporárias para o input do jogador
        static string aux1, aux2, aux3;

        // Variável para saber quando o jogo está a correr
        static bool gameOver = false;

        // Variável para o número de jogadas
        static int numberOfPlays = 0;

        // Mensagem de erro
        const string errorMessage = "----------- Error Message -----------";

        static void Main(string[] args)
        {
            // Chama função de introdução
            Intro();

            //Chama a função que inicia o jogo em si
            Game();
        }

        private static void Game()
        {
            //Variável que define o tamanho do tabuleiro de jogo
            board = new Square[8, 8]; 

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
                        // Resto dos quadrados (é preto e playable)
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
                    WolfTurn();
                }
                
                // Jogadas para A SHEEP
                // Quando o numero de jogadas é par
                else if (numberOfPlays % 2 == 0)
                {   
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("----------------------- FLOCK TURN " +
                                        "----------------------");
                    Console.WriteLine("Choose a Sheep to play:\n");
                    Console.WriteLine("-- S1     S2     S3    S4 --");
                    aux3 = Console.ReadLine().ToUpper();
                    SheepChosen(aux3);
                    if (auxTemp == "invalid")
                        continue;
                    Console.Write($"You chose {aux3}\n");
                    
                    SheepTurn();
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

        // Função que imprime o texto inicial 
        private static void Intro()
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
            Console.WriteLine("-- Insert Wolf's Initial Square Number--");
            Console.WriteLine("-- 1     3     5    7 --");
        }

        // Função que imprime a mensagem de Vitoria
        private static void Victory(string animal, int plays)
        {
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine($"Congratulations, {animal} won the game !!!");
            Console.WriteLine($"Number of total plays = {plays}");
        }
        
        // Só aceita números com +1 ou -1 que a casa atual 
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

        // Função que imprime no ecrã as jogadas possíveis do WOLF
        private static void WolfFreePlays()
        {
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
        }

        // Função que imprime o tabuleiro para o ecrã
        private static void PrintBoard(Square[,] board)
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
                        // Imprime o número à esquerda do tabuleiro
                        // só na linha do meio de cada quadrado
                        if (boardTemp == 1)
                            Console.Write($"{i}   ");
                        else
                            Console.Write($"    ");

                        for (int j = 0; j < 8; j++)
                        {
                                // Imprime WW na posição do wolf
                                // no meio do quadrado
                                if (board[i,j].animal == "Wolf")
                                    if (boardTemp == 1)
                                        Console.Write("  WW  ");
                                    else
                                        Console.Write("      ");

                                // Imprime SX na posição da sheep
                                // no meio do quadrado
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
                                // no meio do quadrado
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
                                // Imprime os números no fim do tabuleiro
                                if (boardTemp == 1)
                                    Console.Write($"   {i}");

                                Console.WriteLine("");
                                // Incrementa a variável para sair do while
                                boardTemp++;                            
                            }
                        }
                    }
                }
                // Imprime números em baixo do tabuleiro
                Console.WriteLine("\n      0     1     2     3     4     5" +
                    "     6     7");
        }
    
        // Verifica se o Wolf tem mais jogadas possíveis
        private static bool WolfGameOver()
        {
            // Se o Wolf estiver no quadrado da esquerda
            if (wolfPos[1] == 0)
            {
                return board[wolfPos[0]+1,wolfPos[1]+1].isPlayable == false && 
                board[wolfPos[0]-1,wolfPos[1]+1].isPlayable == false;
            }
            // Se o Wolf estiver no quadrado da direita
            if (wolfPos[1] == 7)
            {
                return board[wolfPos[0]+1,wolfPos[1]-1].isPlayable == false && 
                board[wolfPos[0]+1,wolfPos[1]-1].isPlayable == false;
            }
            // Se o Wolf estiver no quadrado de cima
            if  (wolfPos[0] == 0)
            {
                return board[wolfPos[1]+1,wolfPos[0]+1].isPlayable == false && 
                board[wolfPos[1]-1,wolfPos[0]+1].isPlayable == false;
            }
            // Se o Wolf estiver no quadrado de baixo
            if  (wolfPos[0] == 7)
            {
                return board[wolfPos[1]+1,wolfPos[0]-1].isPlayable == false && 
                board[wolfPos[1]-1,wolfPos[0]-1].isPlayable == false;
            }
            // Se o Wolf estiver no quadrado da esquerda
            else
            {
                return board[wolfPos[0]+1,wolfPos[1]+1].isPlayable == false &&
                board[wolfPos[0]-1,wolfPos[1]-1].isPlayable == false &&
                board[wolfPos[0]+1,wolfPos[1]-1].isPlayable == false && 
                board[wolfPos[0]-1,wolfPos[1]+1].isPlayable == false;        
            }    
        }

        // Verifica qual SHEEP o jogador escolheu
        // É chamada novamente caso o jogador nao escolha uma das 4 opções
        private static bool SheepGameOver(string aux3)
        {
            if (hasMoved == false)
            {
                if (aux3 == "S1")
                {
                    sheepNewPos[0] = sheep1Pos[0];
                    sheepNewPos[1] = sheep1Pos[1];
                    
                    if (sheepNewPos[1] == 0)
                    {
                        return board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;
                    }
                    if (sheepNewPos[1] == 7 || sheepNewPos[0] == 7 )
                    {
                        
                        return board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;
                    }
                    else
                    {
                        
                        return board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false &&
                        board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;        
                    }
                }
                
                else if (aux3 == "S2")
                {
                    sheepNewPos[0] = sheep2Pos[0];
                    sheepNewPos[1] = sheep2Pos[1];
                    
                    if (sheepNewPos[1] == 0 || sheepNewPos[0] == 0 )
                    {
                        return true;
                    }
                    if (sheepNewPos[1] == 7 || sheepNewPos[0] == 7 )
                    {
                        return board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false && 
                        board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false;
                    }
                    else
                    {
                        return board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false &&
                        board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;        
                    }
                }

                else if (aux3 == "S3")
                {
                    sheepNewPos[0] = sheep3Pos[0];
                    sheepNewPos[1] = sheep3Pos[1];
                    
                    if (sheepNewPos[1] == 0 || sheepNewPos[0] == 0 )
                    {
                        return true;
                    }
                    if (sheepNewPos[1] == 7 || sheepNewPos[0] == 7 )
                    {
                        return board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false && 
                        board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false;
                    }
                    else
                    {
                        return board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false &&
                        board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;        
                    }
                }

                else if (aux3 == "S4")
                {
                    sheepNewPos[0] = sheep4Pos[0];
                    sheepNewPos[1] = sheep4Pos[1];
                    
                    if (sheepNewPos[1] == 0 || sheepNewPos[0] == 0 )
                    {
                        return true;
                    }
                    if (sheepNewPos[1] == 7 || sheepNewPos[0] == 7 )
                    {
                        return board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false && 
                        board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false;
                    }
                    else
                    {
                        return board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false &&
                        board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;        
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (sheepNewPos[1] == 0 || sheepNewPos[0] == 0 )
                    {
                        return true;
                    }
                    if (sheepNewPos[1] == 7 || sheepNewPos[0] == 7 )
                    {
                        return board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false && 
                        board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;
                    }
                    else
                    {
                        return board[sheepNewPos[0]-1,sheepNewPos[1]-1].isPlayable == false &&
                        board[sheepNewPos[0]-1,sheepNewPos[1]+1].isPlayable == false;        
                    }
            }

        }
        private static (int, int, string) SheepChosen(string aux3)
        {
            switch (aux3)
            {
                case "S1":
                    sheepNewPos[0] = sheep1Pos[0];
                    sheepNewPos[1] = sheep1Pos[1];
                    auxTemp = "S1";
                    if(SheepGameOver(aux3))
                    {
                        Console.WriteLine("That sheep is blocked. Pick another one.");
                        auxTemp = "invalid";
                        break;
                    }
                        

                break;
                case "S2":
                    sheepNewPos[0] = sheep2Pos[0];
                    sheepNewPos[1] = sheep2Pos[1];
                    auxTemp = "S2";
                    if(SheepGameOver(aux3))
                    {
                        Console.WriteLine("That sheep is blocked. Pick another one.");
                        auxTemp = "invalid";
                        break;
                    }
                break;
                case "S3":
                    sheepNewPos[0] = sheep3Pos[0];
                    sheepNewPos[1] = sheep3Pos[1];
                    auxTemp = "S3";
                    if(SheepGameOver(aux3))
                    {
                        Console.WriteLine("That sheep is blocked. Pick another one.");
                        auxTemp = "invalid";
                        break;
                    }
                    break;
                case "S4":
                    sheepNewPos[0] = sheep4Pos[0];
                    sheepNewPos[1] = sheep4Pos[1];
                    auxTemp = "S4";
                    if(SheepGameOver(aux3))
                    {
                        Console.WriteLine("That sheep is blocked. Pick another one.");
                        auxTemp = "invalid";
                        break;
                    }
                    break;
                default:
                    Console.WriteLine("Not a Valid Choice, try again.");
                    auxTemp = "invalid";
                    break;
            }
            return (sheepNewPos[0],sheepNewPos[1], auxTemp);
        }
        private static bool CheckConvert(int aux1, string aux2, int numberOfPlays)
        {

            // Mensagem de erro
            const string errorMessage = "----------- INVALID INPUT -----------";
            string errorBar = string.Concat(Enumerable.Repeat("-", 37));

            bool canConvert; 
            try
            {
                aux1 = Convert.ToInt16(aux2);
                canConvert = true;
            }
            catch (OverflowException)
            {
                Console.WriteLine(errorMessage);
                Console.WriteLine("");
                if (numberOfPlays > 0)
                    Console.WriteLine("Please insert a valid number.\n");
                else
                    Console.WriteLine("Please insert a valid initial position.\n");
                canConvert = false;
                Console.WriteLine(errorBar);
            }
            catch (FormatException)
            {
                Console.WriteLine(errorMessage);
                Console.WriteLine("");
                if (numberOfPlays > 0)
                    Console.WriteLine("The input must be a valid line" +
                " number or the 'exit' command.\n");
                else
                    Console.WriteLine("Please insert a number, not a string.\n");
                
                canConvert = false;
                Console.WriteLine(errorBar);
            }

            return canConvert;

        }
    
        // É corrida no primeiro turno
        private static void FirstTurn()
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
                if (CheckConvert(wolfPos[1], aux2, numberOfPlays))
                    wolfPos[1] = Convert.ToInt16(aux2);
                else
                    continue;
                

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
    
        // É corrida no turno do wolf
        private static void WolfTurn()
        {
            do 
            {
                Console.WriteLine(" ");
                Console.WriteLine("----------------------- WOLF TURN " +
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
                // Pedir input ao jogador
                // Imprime que casas podem ser jogadas
                WolfFreePlays();

                Console.Write("\nInsert a row number: ");
                aux1 = Console.ReadLine();

                Console.Write("Insert a column number: ");
                aux2 = Console.ReadLine();

                // Se escrever exit, sai do jogo
                if (aux1 == "exit" || aux2 == "exit")
                {
                    gameOver = true;
                    break;
                }
                if (CheckConvert(wolfNewPos[0], aux1, numberOfPlays))
                    wolfNewPos[0] = Convert.ToInt16(aux1);
                else
                    continue;

                if (CheckConvert(wolfNewPos[1], aux2, numberOfPlays))
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

                        // Define nova posição do lobo e
                        // define como playable = False
                        wolfPos[0] = wolfNewPos[0];
                        wolfPos[1] = wolfNewPos[1];
                        board[wolfPos[0], wolfPos[1]].isPlayable = false;
                        break; 
                    }
                }
                
                // Se o Input não for válido vai repetir o loop
                Console.WriteLine("");
                Console.WriteLine("Please choose a valid number");
            } while(board[wolfPos[0], wolfPos[1]].isPlayable == false);
        }
    
        // É corrida no turno da sheep
        private static void SheepTurn()
        {
            do
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
                
                // Tentar converter o input para int
                // Se não conseguir imprime mensagens de erro
                if (CheckConvert(sheepTempPos[0], aux1, numberOfPlays))
                    sheepTempPos[0] = Convert.ToInt16(aux1);
                else
                    continue;

                if (CheckConvert(sheepTempPos[1], aux2, numberOfPlays))
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
                Console.WriteLine("Please choose a valid number");
            } while(board[sheepNewPos[0], sheepNewPos[1]].isPlayable == false);
                
            // Depois da jogada vai dar o valor da nova posição
            // à Sheep que foi escolhida
            switch (aux3)
            {
                case "S1":
                    hasMoved = true;
                    sheep1Pos[0] = sheepNewPos[0];
                    sheep1Pos[1] = sheepNewPos[1];
                    break;
                case "S2":
                    hasMoved = true;
                    sheep2Pos[0] = sheepNewPos[0];
                    sheep2Pos[1] = sheepNewPos[1];
                    break;
                case "S3":
                    hasMoved = true;
                    sheep3Pos[0] = sheepNewPos[0];
                    sheep3Pos[1] = sheepNewPos[1];
                    break;
                case "S4":
                    hasMoved = true;
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