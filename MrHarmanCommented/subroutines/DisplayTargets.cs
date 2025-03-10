static void DisplayTargets(List<int> Targets)
{
    //Loops through list of Targets with line splitting them
    Console.Write("|");
    foreach (int T in Targets)
    {
        if (T == -1)
        {
            Console.Write(" ");
        }
        else
        {
            Console.Write(T);
        }
        Console.Write("|");
    }
    Console.WriteLine();
    Console.WriteLine();
}
