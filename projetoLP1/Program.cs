using System.Runtime.Serialization;
using System.Reflection.Metadata;
using System.Collections.Concurrent;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Text;
using System.Reflection.Emit;
using System.Data;
using System.Collections;
using System.Linq;
using System;

namespace projetoLP1
{

    public class Square
    {
        // Diz se pode mover para o próximo quadrado
        public bool isPlayable;
        // Linha do tabuleiro
        public int line;
        // Coluna do tabuleiro
        public int column;
        // Animal que vai ocupar o quadrado
        public string animal;

        public Square(int x, int y)
        {
            line = x;
            column = y;
        }

        // public void DrawWolf(int squareCoord)
        // {
            
        // }
    }
    class Program
    {
        static void Main(string[] args)
        {


            
            static int ArithProg(int a_n)
            {
                int n = (a_n + 1)/2;
                return n;
            }
            
            // white square lines
            string patternString = new string(String.Concat(Enumerable.Repeat("#", 7)));
            // black square lines
            string spaceString = new string(String.Concat(Enumerable.Repeat(" ", 7)));

            // wolf string for lines starting with a white square
            string wolfString1 = new string(String.Concat(patternString, ("   W   ")));
            // wolf string for lines starting with a black square
            string wolfString2 = new string(String.Concat(("   W   "), patternString));

            //string for lines starting with a white square
            string line1 = new string(String.Concat(patternString, spaceString));
            //string for lines starting with a black square
            string line2 = new string(String.Concat(spaceString, patternString));

            // keeps track of the drawn lines (NOT THE BOARD LINES)
            int lineCounter = 0;
            // keeps track of the number of lines on the board
            int boardLineNum = 1;
            int j;
            // allows reference to the lines as they are visually represented 
            int lineModifier = 3;
            int numCount = 1;


            int wolfLine = 7;
            int wolfColumn = 2;
            int auxWolfColumn = wolfColumn;
            int lastWolfLine = wolfLine;
            int lastWolfColumn = 2;
            bool gameOver = false;
            Square[,] board;
            board = new Square[8, 8];

            while (gameOver == false)
            {
                // cria o board com a classe Square
                // Criação de cada Square
                for (int i = 0; i < 8; i++)
                {
                    for (j = 0; j < 8; j++)
                    {
                        // Cria uma instância da classe Square para cada ponto  
                        // no tabuleiro
                        board[i,j] = new Square(i, j);

                        // Dá valores à line e column de cada instância do Square
                        board[i,j].line = i;
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
                
                // Verifies if user is going out of boundaries  
                if ((auxWolfColumn - lastWolfColumn) <= -2 || (auxWolfColumn - lastWolfColumn) >= 2 || (wolfLine - lastWolfLine) <= -2 || (wolfLine - lastWolfLine) >= 2)
                {
                    Console.WriteLine("That's not a valid move.");

                    // Keeps the wolf at the same square, not considering the invalid input to move the it
                    wolfLine = lastWolfLine;
                    auxWolfColumn = lastWolfColumn;
                }

                // Verifies if user is trying to go to a white square
                 
                if (board[wolfLine - 1, auxWolfColumn - 1].isPlayable == false)
                {
                    // drawWolf = false;
                    Console.WriteLine($"{wolfLine}, {auxWolfColumn} is not a valid square;");
                    wolfLine = lastWolfLine;
                    auxWolfColumn = lastWolfColumn;

                }


                if (wolfLine % 2 != 0)
                {
                    if (auxWolfColumn % 2 == 0)
                    {
                        wolfColumn = auxWolfColumn / 2;
                    }
                }
                else
                {
                    wolfColumn = ArithProg(auxWolfColumn);
                }

                Console.WriteLine("       1      2      3      4      5      6      7      8");
                Console.WriteLine("  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  * *");

                for (int i = 0; i < 8 * lineModifier; i++)
                {                    
                    if (i >= 1)
                        numCount++;

                    if (numCount == 1 && i != 0)
                        Console.Write("{0} * ", (boardLineNum));

                    else
                        Console.Write("  * ");

                    if (i % 3 == 0)
                        numCount = 0;
                    


                    for (j = 0; j < 4; j++)
                    {  

                        if((boardLineNum - 1) % 2 == 0)
                        {
                            
                            // checks if the loop is in the middle of the square and, if the wolf is there, i'll be drawn on the square
                            if ((numCount == 1 && i != 0) && (j == wolfColumn - 1) && (boardLineNum) == wolfLine && board[wolfLine - 1, auxWolfColumn - 1].isPlayable == true)
                            {
                                board[lastWolfLine, lastWolfColumn].isPlayable = true;
                                board[wolfLine - 1, auxWolfColumn - 1].isPlayable = false;
                                Console.Write(wolfString1);
                            }
                            
                            // draws the lines of the board lines if it starts with a white square
                            else
                            {
                                Console.Write(line1);
                            }
                            
                        }
                        else
                        {
                            
                            // checks if the loop is in the middle of the square and, if the wolf is there, i'll be drawn on the square
                            if ((numCount == 1 && i != 0) && (j == wolfColumn - 1) && (boardLineNum) == wolfLine && board[wolfLine - 1, auxWolfColumn - 1].isPlayable == true)
                            {
                                board[lastWolfLine, lastWolfColumn].isPlayable = true;
                                board[wolfLine - 1, auxWolfColumn - 1].isPlayable = false;
                                Console.Write(wolfString2);
                            }
                            // draws the lines of the board lines if it starts with a black square
                            else
                            {
                                Console.Write(line2);
                            }
                        }


                    }
                    Console.Write(" *");
                    
                    // increments the line counter
                    lineCounter++;
                    // Console.Write("  lineCounter: {0}", lineCounter);

                    // when the line counter is a multiple of 3, it means that a board line was fully drawn
                    if (lineCounter % 3 == 0)
                    {   
                        // increments the BOARD line numbers
                        boardLineNum++;
                    }
                    
                
                    Console.WriteLine("");
                }



                boardLineNum = 1;
                lineCounter = 0;
                Console.WriteLine("  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  * *");
                Console.WriteLine("");
                lastWolfLine = wolfLine;
                lastWolfColumn = auxWolfColumn;
                    do 
                    {
                        Console.WriteLine("");
                        Console.WriteLine("");
                        
                        Console.WriteLine("----------- WOLF TURN -----------");
                        string aux1;
                        string aux2;
                        // Pedir input ao jogador
                        Console.Write("Insert line number: ");
                        // wolfLine = Convert.ToInt32(Console.ReadLine());
                        aux1 = Console.ReadLine();
                        Console.Write("Insert Column number: ");
                        aux2 = Console.ReadLine();
                        // auxWolfColumn = Convert.ToInt32(Console.ReadLine());

                        // Se escrever exit, sai do jogo
                        if (aux1 == "exit" || aux2 == "exit")
                        {
                            gameOver = true;
                            break;  
                        }
                        
                        //Converte a string do input para int
                        wolfLine = Convert.ToInt16(aux1);
                        auxWolfColumn = Convert.ToInt16(aux2);
                        // Se meter o número valido, sai do loop
                        if (board[wolfLine - 1, auxWolfColumn - 1].isPlayable)
                            break;
                        
                        // Se não for válido vai repetir o processo
                        Console.WriteLine("Please choose a valid number");
                    } while(board[wolfLine - 1, auxWolfColumn - 1].isPlayable == false);

                    board[wolfLine - 1, auxWolfColumn - 1].animal = "Wolf";

 
                
                // wolfLine = Convert.ToInt32(Console.ReadLine());
                
                // auxWolfColumn = Convert.ToInt32(Console.ReadLine());




            }   


        }
    }
}
