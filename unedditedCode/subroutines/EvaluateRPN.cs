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
    if (Convert.ToDouble(S[0]) - Math.Truncate(Convert.ToDouble(S[0])) == 0.0)
    {
        return (int)Math.Truncate(Convert.ToDouble(S[0]));
    }
    else
    {
        return -1;
    }
}