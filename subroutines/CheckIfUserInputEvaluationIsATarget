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
