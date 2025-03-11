static void DisplayTargets(List<int> Targets)
{
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