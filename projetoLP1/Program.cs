using System.Buffers;
using System.ComponentModel;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Diagnostics.Contracts;
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

            static void InvalidMove(string type)
            {
                string divisionMsg = "----------------- INVALID MOVE ----------------";
                string division = "-----------------------------------------------";
                string differentChoose = "Please choose a different square.";
                Console.WriteLine(divisionMsg);
                switch (type)
                {
                    case "sameSquare":
                        Console.WriteLine("You are already on that square.");
                        Console.WriteLine(differentChoose);
                        break;
                    case "farSquare":
                        Console.WriteLine("That square is too far from where you are.");
                        Console.WriteLine(differentChoose);
                        break;
                    case "firstMove":
                        Console.WriteLine("The wolf can only start on the 1st line.");
                        Console.WriteLine(differentChoose);
                        break;
                    case "unavailableSquare":
                        Console.WriteLine("That square is not available.");
                        Console.WriteLine(differentChoose);
                        break;

                }
                Console.WriteLine(division);
                
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
            string invalidMoveMsg;
            // keeps track of the drawn lines (NOT THE BOARD LINES)
            int lineCounter = 0;
            // keeps track of the number of lines on the board
            int boardLineNum = 1;
            int j;
            // allows reference to the lines as they are visually represented 
            int lineModifier = 3;
            int numCount = 1;

            int round = 0;
            int wolfLine = 1;
            int wolfColumn = 8;
            int auxWolfColumn = 6;
            int lastWolfLine = 1;
            int lastWolfColumn = 6;
            bool gameOver = false;
            Square[,] board;
            board = new Square[8, 8];


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


            int[] InputDealer(int round, int lastWolfLine, int lastWolfColumn)
            {
                string aux1;
                string aux2;
                wolfLine = 1; 
                auxWolfColumn = 1;
                // int[] wolfCoords = new int[2] {wolfLine, auxWolfColumn};
                int[] wolfCoords = new int[4];
                if (round % 2 == 0 || round % 2 != 0)
                {
                    // code to deal with the input for the wolf
                    while(board[wolfLine - 1, auxWolfColumn - 1].isPlayable == false)
                    {
                        if (round == 0)
                            Console.WriteLine("Where do you want the wolf to start from?");
                        
                        // Ask to insert line
                        Console.Write("Insert line number: ");
                        aux1 = Console.ReadLine();
                        // Ask to insert column 
                        Console.Write("Insert Column number: ");
                        aux2 = Console.ReadLine();

                        //Convert string input to short
                        wolfLine = Convert.ToInt16(aux1);
                        auxWolfColumn = Convert.ToInt16(aux2);
                        
                        Console.WriteLine("playable: {0}", board[wolfLine - 1, auxWolfColumn - 1].isPlayable);

                        // Deal with the general wolf positioning
                        if (round != 0)
                        {
                            // If the insert wolfCoords are valid the loop breaks

                            if (lastWolfLine == wolfLine && lastWolfColumn == auxWolfColumn)
                                InvalidMove("sameSquare");

                            else if (board[wolfLine - 1, auxWolfColumn - 1].isPlayable)
                                {
                                    wolfCoords[0] = wolfLine;
                                    wolfCoords[1] = auxWolfColumn;
                                }
                            
                            Console.WriteLine("lastWolfLine: {0} | lastWolfColumn: {1}", lastWolfLine,lastWolfColumn);
                            if ((auxWolfColumn - lastWolfColumn) <= -2 || 
                                (auxWolfColumn - lastWolfColumn) >= 2 || 
                                (wolfLine - lastWolfLine) <= -2 || 
                                (wolfLine - lastWolfLine) >= 2)
                                {
                                    InvalidMove("unavailableSquare");
                                    wolfLine = lastWolfLine;
                                    auxWolfColumn = lastWolfColumn;
                                    continue;
                                }
                        }

                        // Deal with the wolf initial positioning
                        else
                        {
                                if(wolfLine > 1)
                                {
                                    InvalidMove("firstMove");
                                    continue;
                                }
                                    
                                else if (auxWolfColumn % 2 == 0)
                                {
                                    lastWolfLine = wolfLine;
                                    lastWolfColumn = auxWolfColumn;
                                    wolfCoords[0] = wolfLine;
                                    wolfCoords[1] = auxWolfColumn;
                                    wolfCoords[2] = lastWolfLine;
                                    wolfCoords[3] = lastWolfColumn;
                                    break;
                                }

                        }
                    } 
                    board[wolfLine - 1, auxWolfColumn - 1].animal = "Wolf";
                    // wolfCoords = new int [2] {wolfLine, auxWolfColumn};
                    return wolfCoords;
                }

                else
                {
                    wolfCoords[0] = 1;
                    wolfCoords[1] = 2;
                    return wolfCoords;
                    // code to deal with the sheep input    
                }
            }

            int [] wolfCoords = new int [4];

            while (gameOver == false)
            {
                if (round == 0)
                {   
                    InputDealer(round, 1, 1).CopyTo(wolfCoords, 0);
                    wolfLine = wolfCoords[0];
                    auxWolfColumn = wolfCoords[1];
                    lastWolfLine = wolfCoords[2];
                    lastWolfColumn = wolfCoords[3];
                    round++;
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
                                board[lastWolfLine - 1, lastWolfColumn - 1].isPlayable = true;
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
                                board[lastWolfLine - 1, lastWolfColumn - 1].isPlayable = true;
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
                Console.WriteLine("round: {0}", round);
                lastWolfLine = wolfLine;
                lastWolfColumn = auxWolfColumn;

                // Gets user input
                InputDealer(round, lastWolfLine, lastWolfColumn).CopyTo(wolfCoords, 0);
                wolfLine = wolfCoords[0];
                auxWolfColumn = wolfCoords[1];


                // increments rounds
                if (board[wolfLine - 1, auxWolfColumn - 1].isPlayable)
                    round++;
            }   
        }
    }
}
