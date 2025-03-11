static bool CheckIfUserInputValid(string UserInput)
{
    return Regex.IsMatch(UserInput, @"^([0-9]+[\+\-\*\/])+[0-9]+$");
}