//Skeleton Program code for the AQA A Level Paper 1 Summer 2025 examination
//this code should be used in conjunction with the Preliminary Material
//written by the AQA Programmer Team
//developed in the Visual Studio Community Edition programming environment

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TargetClearCS
{
    internal class Program
    {
        static Random RGen = new Random();

        static void Main(string[] args)
        {
            List<int> NumbersAllowed = new List<int>();
            List<int> Targets;
            int MaxNumberOfTargets = 20;
            int MaxTarget;
            int MaxNumber;
            bool TrainingGame;
            Console.Write("Enter y to play the training game, anything else to play a random game: ");
            string Choice = Console.ReadLine().ToLower();
            Console.WriteLine();
            if (Choice == "y")
            {
                MaxNumber = 1000;
                MaxTarget = 1000;
                TrainingGame = true;
                Targets = new List<int> { -1, -1, -1, -1, -1, 23, 9, 140, 82, 121, 34, 45, 68, 75, 34, 23, 119, 43, 23, 119 };
            }
            else
            {
                MaxNumber = 10;
                MaxTarget = 50;
                TrainingGame = false;
                //Generates list of number, maxnumberoftargets = original length of list
                Targets = CreateTargets(MaxNumberOfTargets, MaxTarget);
            }
            NumbersAllowed = FillNumbers(NumbersAllowed, TrainingGame, MaxNumber);
            PlayGame(Targets, NumbersAllowed, TrainingGame, MaxTarget, MaxNumber);
            Console.ReadLine();
        }

        //Subroutine to play the game
        static void PlayGame(List<int> Targets, List<int> NumbersAllowed, bool TrainingGame, int MaxTarget, int MaxNumber)
        {
            int Score = 0;
            bool GameOver = false;
            string UserInput;
            List<string> UserInputInRPN;
            while (!GameOver)
            {
                //Displays the whole game
                DisplayState(Targets, NumbersAllowed, Score);
                Console.Write("Enter an expression: ");
                UserInput = Console.ReadLine();
                Console.WriteLine();
                if (CheckIfUserInputValid(UserInput))
                //Checks characters used, if calculation is possible
                {
                    //converts userinput to RPN
                    UserInputInRPN = ConvertToRPN(UserInput);
                    //if the numbers exist in the numbers allowed
                    if (CheckNumbersUsedAreAllInNumbersAllowed(NumbersAllowed, UserInputInRPN, MaxNumber))
                    {
                        //check if the answer is in targets list
                        if (CheckIfUserInputEvaluationIsATarget(Targets, UserInputInRPN, ref Score))
                        {
                            RemoveNumbersUsed(UserInput, MaxNumber, NumbersAllowed);
                            NumbersAllowed = FillNumbers(NumbersAllowed, TrainingGame, MaxNumber);
                        }
                    }
                }
                Score--;

                //if the start of the targets list is no longer -1, the game is over
                if (Targets[0] != -1)
                {
                    GameOver = true;
                }
                else
                {
                    UpdateTargets(Targets, TrainingGame, MaxTarget);
                }
            }
            Console.WriteLine("Game over!");
            DisplayScore(Score);
        }
        static bool CheckIfUserInputEvaluationIsATarget(List<int> Targets, List<string> UserInputInRPN, ref int Score)
        {
            //returns answer to the value
            int UserInputEvaluation = EvaluateRPN(UserInputInRPN);

            bool UserInputEvaluationIsATarget = false;

            if (UserInputEvaluation != -1)
            {
                for (int Count = 0; Count < Targets.Count; Count++)
                {
                    if (Targets[Count] == UserInputEvaluation)
                    {
                        Score += 2;
                        Targets[Count] = -1;
                        UserInputEvaluationIsATarget = true;
                    }
                }
            }
            return UserInputEvaluationIsATarget;
        }

        //remove numbers used in operation
        static void RemoveNumbersUsed(string UserInput, int MaxNumber, List<int> NumbersAllowed)
        {
            List<string> UserInputInRPN = ConvertToRPN(UserInput);
            foreach (string Item in UserInputInRPN)
            {
                if (CheckValidNumber(Item, MaxNumber))
                {
                    if (NumbersAllowed.Contains(Convert.ToInt32(Item)))
                    {
                        NumbersAllowed.Remove(Convert.ToInt32(Item));
                    }
                }
            }
        }
        static void UpdateTargets(List<int> Targets, bool TrainingGame, int MaxTarget)
        {
            for (int Count = 0; Count < Targets.Count - 1; Count++)
            {
                Targets[Count] = Targets[Count + 1];
            }
            Targets.RemoveAt(Targets.Count - 1);
            if (TrainingGame)
            {
                Targets.Add(Targets[Targets.Count - 1]);
            }
            else
            {
                Targets.Add(GetTarget(MaxTarget));
            }
        }

        static bool CheckNumbersUsedAreAllInNumbersAllowed(List<int> NumbersAllowed, List<string> UserInputInRPN, int MaxNumber)
        {
            List<int> Temp = new List<int>();

            foreach (int Item in NumbersAllowed)
            {
                Temp.Add(Item);
            }
            //loops through every number and operator in the RPN
            foreach (string Item in UserInputInRPN)
            {
                if (CheckValidNumber(Item, MaxNumber))
                {
                    //if item from userInputRPN doesn't exist in numbersAllowed, stop looping and return false
                    if (Temp.Contains(Convert.ToInt32(Item)))
                    {
                        Temp.Remove(Convert.ToInt32(Item));
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //check is a number is between 0 to 9 checks if number is lower than max number
        static bool CheckValidNumber(string Item, int MaxNumber)
        {
            if (Regex.IsMatch(Item, "^[0-9]+$"))
            {
                int ItemAsInteger = Convert.ToInt32(Item);
                if (ItemAsInteger > 0 && ItemAsInteger <= MaxNumber)
                {
                    return true;
                }
            }
            return false;
        }
        //displays the targets, score and numbers allowed
        static void DisplayState(List<int> Targets, List<int> NumbersAllowed, int Score)
        {
            DisplayTargets(Targets);
            DisplayNumbersAllowed(NumbersAllowed);
            DisplayScore(Score);
        }

        static void DisplayScore(int Score)
        {
            //displays score and adds two lines underneath
            Console.WriteLine($"Current score: {Score}");
            Console.WriteLine();
            Console.WriteLine();
        }

        static void DisplayNumbersAllowed(List<int> NumbersAllowed)
        {
            //loops through numbers available list
            Console.Write("Numbers available: ");
            foreach (int Number in NumbersAllowed)
            {
                //Displays number variable
                Console.Write($"{Number}  ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        static void DisplayTargets(List<int> Targets)
        {
            //Loops through list of Targets with line splitting them
            Console.Write("|");
            foreach (int T in Targets)
            {
                if (T == -1)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(T);
                }
                Console.Write("|");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        //Convert to reverse polish notation
        //Add brackets and change subroutine to be able to allow brackets
        static List<string> ConvertToRPN(string UserInput)
        {
            int Position = 0;
            //Dictionary in charge of order of how sum is calculated
            Dictionary<string, int> Precedence = new Dictionary<string, int>
            {
                { "+", 2 }, { "-", 2 }, { "*", 4 }, { "/", 4 }
            };
            List<string> Operators = new List<string>();

            //seperates a number from the user input
            int Operand = GetNumberFromUserInput(UserInput, ref Position);

            //adds that number to the previous inputs
            List<string> UserInputInRPN = new List<string> { Operand.ToString() };
            //Two Lists; Operator and OPERAND

            //Adds operator to operator list
            Operators.Add(UserInput[Position - 1].ToString());

            while (Position < UserInput.Length)
            {
                Operand = GetNumberFromUserInput(UserInput, ref Position);
                UserInputInRPN.Add(Operand.ToString());
                //check if we are at the end of the userinput
                if (Position < UserInput.Length)
                {
                    string CurrentOperator = UserInput[Position - 1].ToString();

                    //while there is still an operator in the operator list and the previous operator has a greater precidence than the current operator
                    while (Operators.Count > 0 && Precedence[Operators[Operators.Count - 1]] > Precedence[CurrentOperator])
                    {
                        //add operator from the operators list to list that will be returned
                        UserInputInRPN.Add(Operators[Operators.Count - 1]);
                        //remove that operator from the operators list
                        Operators.RemoveAt(Operators.Count - 1);
                    }

                    //if operators still exist in the list, and the previous operator has the same precedence as current operator

                    if (Operators.Count > 0 && Precedence[Operators[Operators.Count - 1]] == Precedence[CurrentOperator])
                    {
                        //add operator from the operators list to list that will be returned
                        UserInputInRPN.Add(Operators[Operators.Count - 1]);
                        //remove that operator from the operators list
                        Operators.RemoveAt(Operators.Count - 1);
                    }

                    Operators.Add(CurrentOperator);
                }
                //this happens when we have reached the end of the userinput,
                else
                {
                    //loop through the rest of the remaining operators to add to the list
                    while (Operators.Count > 0)
                    {
                        UserInputInRPN.Add(Operators[Operators.Count - 1]);
                        Operators.RemoveAt(Operators.Count - 1);
                    }
                }
            }
            return UserInputInRPN;
        }

        //return the answer to the userInput
        static int EvaluateRPN(List<string> UserInputInRPN)
        {
            List<string> S = new List<string>();
            while (UserInputInRPN.Count > 0)
            {
                while (!"+-*/".Contains(UserInputInRPN[0]))
                {
                    S.Add(UserInputInRPN[0]);
                    UserInputInRPN.RemoveAt(0);
                }
                double Num2 = Convert.ToDouble(S[S.Count - 1]);
                S.RemoveAt(S.Count - 1);
                double Num1 = Convert.ToDouble(S[S.Count - 1]);
                S.RemoveAt(S.Count - 1);
                double Result = 0;
                switch (UserInputInRPN[0])
                {
                    case "+":
                        Result = Num1 + Num2;
                        break;
                    case "-":
                        Result = Num1 - Num2;
                        break;
                    case "*":
                        Result = Num1 * Num2;
                        break;
                    case "/":
                        Result = Num1 / Num2;
                        break;
                }
                UserInputInRPN.RemoveAt(0);
                S.Add(Convert.ToString(Result));
            }
            //if the answer is a decimal number, return -1
            if (Convert.ToDouble(S[0]) - Math.Truncate(Convert.ToDouble(S[0])) == 0.0)
            {
                return (int)Math.Truncate(Convert.ToDouble(S[0]));
            }
            else
            {
                return -1;
            }
        }

        static int GetNumberFromUserInput(string UserInput, ref int Position)
        {
            string Number = "";
            bool MoreDigits = true;
            while (MoreDigits)
            {
                //Loops through to assure 1 number is used before operator
                if (Regex.IsMatch(UserInput[Position].ToString(), "[0-9]"))
                {
                    Number += UserInput[Position];
                }
                else
                {
                    MoreDigits = false;
                }
                Position++;
                if (Position == UserInput.Length)
                //reached end of list
                {
                    MoreDigits = false;
                }
            }
            if (Number == "")
            {
                return -1;
            }
            else
            {
                return Convert.ToInt32(Number);
            }
        }

        static bool CheckIfUserInputValid(string UserInput)
        {
            //Checks character requirements
            return Regex.IsMatch(UserInput, @"^([0-9]+[\+\-\*\/])+[0-9]+$");
        }

        //choose a random number, maxtarget is highest number
        static int GetTarget(int MaxTarget)
        {
            return RGen.Next(MaxTarget) + 1;
        }

        //redudent code?
        static int GetNumber(int MaxNumber)
        {
            return RGen.Next(MaxNumber) + 1;
        }

        //creates targets
        static List<int> CreateTargets(int SizeOfTargets, int MaxTarget)
        {
            List<int> Targets = new List<int>();
            for (int Count = 1; Count <= 5; Count++)
            {
                //Adds to List of Targets, int, cant be 0, first 5 empty
                Targets.Add(-1);
            }
            for (int Count = 1; Count <= SizeOfTargets - 5; Count++)
            {
                //random numbers placed into rest of list til SizeOfTargets
                Targets.Add(GetTarget(MaxTarget));
            }
            return Targets;
        }

        //add more numbers to the numbersAllowedList
        static List<int> FillNumbers(List<int> NumbersAllowed, bool TrainingGame, int MaxNumber)
        {
            if (TrainingGame)
            {
                return new List<int> { 2, 3, 2, 8, 512 };
            }
            else
            {
                while (NumbersAllowed.Count < 5)
                {
                    NumbersAllowed.Add(GetNumber(MaxNumber));
                }
                return NumbersAllowed;
            }
        }
    }
}



