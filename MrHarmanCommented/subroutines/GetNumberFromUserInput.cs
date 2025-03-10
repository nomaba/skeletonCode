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
