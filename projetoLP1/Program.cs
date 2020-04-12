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


            // Square quadrado = new Square(false, "wolf");
            // Console.WriteLine(quadrado.available);




            
            string patternString = new string(String.Concat(Enumerable.Repeat("#", 7)));
            string spaceString = new string(String.Concat(Enumerable.Repeat(" ", 7)));


            string wolfString1 = new string(String.Concat(patternString, ("   W   ")));
            string wolfString2 = new string(String.Concat(("   W   "), patternString));


            string line1 = new string(String.Concat(patternString, spaceString));
            string line2 = new string(String.Concat(spaceString, patternString));


            int counter = 0;
            int lineCounter = 0;
            int j;
            int lineModifier = 3;
            int identifierNum = 0;
            int numCounter = 1;
            bool drawWolf = true;
            int wolfLine = 2;
            int wolfColumn = 8;
            

            static int ArithProg(int a_n)
            {
                int n = (a_n + 1)/2;
                return n;
            }

            // Console.WriteLine(ArithProg(5));


            if (wolfLine % 2 != 0)
            {
                if (wolfColumn % 2 == 0)
                {
                    wolfColumn = wolfColumn / 2;
                }
            }
            else
            {
                wolfColumn = ArithProg(wolfColumn);
            }

            for (int i = 0; i < 8 * lineModifier; i++)
            {
                
                // Console.Write("{0}  ",lineCounter);
                
                if (i >= 1)
                {
                    numCounter++;
                }
                if (numCounter == 1 && i != 0)
                {
                    Console.Write("{0} * ", (lineCounter + 1));
                    
                }
                else
                {
                    Console.Write("  * ");
                }
                if (i % 3 == 0)
                {
                    numCounter = 0;
                }


                for (j = 0; j < 4; j++)
                {  
                    // Console.WriteLine(Convert.ToDouble(i) / 3.0f); 
                    // j is the horizontal identifier for the squares
                    if(lineCounter % 2 == 0)
                    {
                        // Console.Write(lineCounter + 1);

                        if ((numCounter == 1 && i != 0) && (j == wolfColumn - 1) && (lineCounter + 1) == wolfLine)
                        {
                            Console.Write(wolfString1);
                        }
                        
                        else
                        {
                            Console.Write(line1);
                        }
                        
                    }
                    else
                    {
                        // Console.Write(line2);

                        if ((numCounter == 1 && i != 0) && (j == wolfColumn - 1) && (lineCounter + 1) == wolfLine)
                        {
                            Console.Write(wolfString2);
                        }
                        
                        else
                        {
                            Console.Write(line2);
                        }
                    }


                }
                
                counter++;
                if (counter % 3 == 0 && counter > 0)
                {
                    lineCounter++;
                }
                
                
                Console.WriteLine("");
            }









        }
    }
}
