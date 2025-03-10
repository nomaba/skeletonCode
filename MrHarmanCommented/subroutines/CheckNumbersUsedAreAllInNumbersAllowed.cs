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
