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
