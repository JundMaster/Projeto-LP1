﻿using System.Runtime.Serialization;
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
        public bool available;
        public string piece;
        
        public Square(bool availableTemp, string pieceTemp)
        {
            available = availableTemp;
            piece = pieceTemp;
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


            string line1 = new string(String.Concat(patternString, spaceString));
            string line2 = new string(String.Concat(spaceString, patternString));

            // keeps track of the drawn line (NOT THE BOARD LINES)
            int lineCounter = 0;
            // keeps track of the number of lines on the board
            int boardLineNum = 1;
            int j;
            int lineModifier = 3;
            int numCount = 1;
            int wolfLine = 1;
            int wolfColumn = 2;
            int auxWolfColumn = 2;
            int lastWolfLine = 1;
            int lastWolfColumn = 2;
            bool done = false;

            while (done == false)
            {
                

                Console.WriteLine(auxWolfColumn - lastWolfColumn);

                if ((auxWolfColumn - lastWolfColumn) <= -2 || (auxWolfColumn - lastWolfColumn) >= 2 || (wolfLine - lastWolfLine) <= -2 || (wolfLine - lastWolfLine) >= 2)
                {
                    Console.WriteLine("That's not a valid move.");
                    wolfLine = lastWolfLine;
                    auxWolfColumn = lastWolfColumn;
                }

                // Verifies if user is trying to go to a valid square
                if (wolfLine % 2 == 0 && auxWolfColumn % 2 == 0)
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
                    {
                        numCount++;
                    }
                    if (numCount == 1 && i != 0)
                    {
                        Console.Write("{0} * ", (boardLineNum));
                        
                    }
                    else
                    {
                        Console.Write("  * ");
                    }
                    if (i % 3 == 0)
                    {
                        numCount = 0;
                    }


                    for (j = 0; j < 4; j++)
                    {  

                        if((boardLineNum - 1) % 2 == 0)
                        {
                            
                            // checks if the loop is in the middle of the square and, if the wolf is there, i'll be drawn on the square
                            if ((numCount == 1 && i != 0) && (j == wolfColumn - 1) && (boardLineNum) == wolfLine)
                            {
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
                            if ((numCount == 1 && i != 0) && (j == wolfColumn - 1) && (boardLineNum) == wolfLine)
                            {
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
                Console.WriteLine($"{lastWolfLine}, {lastWolfColumn}");
                Console.Write("Insert line number: ");
                wolfLine = Convert.ToInt32(Console.ReadLine());
                Console.Write("Insert Column number: ");
                auxWolfColumn = Convert.ToInt32(Console.ReadLine());




            }


        }
    }
}