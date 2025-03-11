        static void PlayGame(List<int> Targets, List<int> NumbersAllowed, bool TrainingGame, int MaxTarget, int MaxNumber)
        {
            int Score = 0;
            bool GameOver = false;
            string UserInput;
            List<string> UserInputInRPN;
            while (!GameOver)
            {
                DisplayState(Targets, NumbersAllowed, Score);
                Console.Write("Enter an expression: ");
                UserInput = Console.ReadLine();
                Console.WriteLine();
                if (CheckIfUserInputValid(UserInput))
                {
                    UserInputInRPN = ConvertToRPN(UserInput);
                    if (CheckNumbersUsedAreAllInNumbersAllowed(NumbersAllowed, UserInputInRPN, MaxNumber))
                    {
                        if (CheckIfUserInputEvaluationIsATarget(Targets, UserInputInRPN, ref Score))
                        {
                            RemoveNumbersUsed(UserInput, MaxNumber, NumbersAllowed);
                            NumbersAllowed = FillNumbers(NumbersAllowed, TrainingGame, MaxNumber);
                        }
                    }
                }
                Score--;
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