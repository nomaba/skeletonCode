        static List<string> ConvertToRPN(string UserInput)
        {
            int Position = 0;
            Dictionary<string, int> Precedence = new Dictionary<string, int>
            {
                { "+", 2 }, { "-", 2 }, { "*", 4 }, { "/", 4 }
            };
            List<string> Operators = new List<string>();
            int Operand = GetNumberFromUserInput(UserInput, ref Position);
            List<string> UserInputInRPN = new List<string> { Operand.ToString() };
            Operators.Add(UserInput[Position - 1].ToString());
            while (Position < UserInput.Length)
            {
                Operand = GetNumberFromUserInput(UserInput, ref Position);
                UserInputInRPN.Add(Operand.ToString());
                if (Position < UserInput.Length)
                {
                    string CurrentOperator = UserInput[Position - 1].ToString();
                    while (Operators.Count > 0 && Precedence[Operators[Operators.Count - 1]] > Precedence[CurrentOperator])
                    {
                        UserInputInRPN.Add(Operators[Operators.Count - 1]);
                        Operators.RemoveAt(Operators.Count - 1);
                    }
                    if (Operators.Count > 0 && Precedence[Operators[Operators.Count - 1]] == Precedence[CurrentOperator])
                    {
                        UserInputInRPN.Add(Operators[Operators.Count - 1]);
                        Operators.RemoveAt(Operators.Count - 1);
                    }
                    Operators.Add(CurrentOperator);
                }
                else
                {
                    while (Operators.Count > 0)
                    {
                        UserInputInRPN.Add(Operators[Operators.Count - 1]);
                        Operators.RemoveAt(Operators.Count - 1);
                    }
                }
            }
            return UserInputInRPN;
        }