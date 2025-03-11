static List<int> FillNumbers(List<int> NumbersAllowed, bool TrainingGame, int MaxNumber)
{
    if (TrainingGame)
    {
        return new List<int> { 2, 3, 2, 8, 512 };
    }
    else
    {
        while (NumbersAllowed.Count < 5)
        {
            NumbersAllowed.Add(GetNumber(MaxNumber));
        }
        return NumbersAllowed;
    }
}