static int GetNumberFromUserInput(string UserInput, ref int Position)
{
    string Number = "";
    bool MoreDigits = true;
    while (MoreDigits)
    {
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