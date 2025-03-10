static void DisplayNumbersAllowed(List<int> NumbersAllowed)
{
    //loops through numbers available list
    Console.Write("Numbers available: ");
    foreach (int Number in NumbersAllowed)
    {
        //Displays number variable
        Console.Write($"{Number}  ");
    }
    Console.WriteLine();
    Console.WriteLine();
}
