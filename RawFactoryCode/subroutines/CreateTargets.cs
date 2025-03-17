static List<int> CreateTargets(int SizeOfTargets, int MaxTarget)
{
    List<int> Targets = new List<int>();
    for (int Count = 1; Count <= 5; Count++)
    {
        Targets.Add(-1);
    }
    for (int Count = 1; Count <= SizeOfTargets - 5; Count++)
    {
        Targets.Add(GetTarget(MaxTarget));
    }
    return Targets;
}