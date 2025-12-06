using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLE
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your string"); // ask for string
            string userInputString = Console.ReadLine(); // get string
            int userInputStringLength = userInputString.Length; // get string length
            int runLengthCounter = 0; // set counter to 0 
            string output = "";
            int FinalCharRunLengthCounter = 0;






            for (int i = 0; i < userInputStringLength-1; i++)
            {
                if (userInputString[i] == userInputString[i + 1])
                {
                    runLengthCounter = runLengthCounter + 1;
                    FinalCharRunLengthCounter = 0;
                }
                else
                {
                                 
                    output = output + (userInputString[i].ToString() + (runLengthCounter + 1).ToString());
                    FinalCharRunLengthCounter = runLengthCounter;
                    runLengthCounter = 0;

                }
                
            }

            output = output + userInputString[userInputStringLength-1].ToString() + FinalCharRunLengthCounter.ToString();


            Console.Write(output);
            Console.ReadLine();
        }
    }
}
