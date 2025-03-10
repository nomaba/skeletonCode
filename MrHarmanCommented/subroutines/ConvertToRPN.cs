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
