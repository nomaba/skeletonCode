//creates targets
static List<int> CreateTargets(int SizeOfTargets, int MaxTarget)
{
    List<int> Targets = new List<int>();
    for (int Count = 1; Count <= 5; Count++)
    {
        //Adds to List of Targets, int, cant be 0, first 5 empty
        Targets.Add(-1);
    }
    for (int Count = 1; Count <= SizeOfTargets - 5; Count++)
    {
        //random numbers placed into rest of list til SizeOfTargets
        Targets.Add(GetTarget(MaxTarget));
    }
    return Targets;
}
