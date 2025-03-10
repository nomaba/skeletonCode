//check is a number is between 0 to 9 checks if number is lower than max number
static bool CheckValidNumber(string Item, int MaxNumber)
{
    if (Regex.IsMatch(Item, "^[0-9]+$"))
    {
        int ItemAsInteger = Convert.ToInt32(Item);
        if (ItemAsInteger > 0 && ItemAsInteger <= MaxNumber)
        {
            return true;
        }
    }
    return false;
}
