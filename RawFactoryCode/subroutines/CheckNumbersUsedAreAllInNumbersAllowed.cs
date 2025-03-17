static bool CheckNumbersUsedAreAllInNumbersAllowed(List<int> NumbersAllowed, List<string> UserInputInRPN, int MaxNumber)
{
    List<int> Temp = new List<int>();
    foreach (int Item in NumbersAllowed)
    {
        Temp.Add(Item);
    }
    foreach (string Item in UserInputInRPN)
    {
        if (CheckValidNumber(Item, MaxNumber))
        {
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