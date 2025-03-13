public bool swapMade = true;
public bool needSwap = false;

static void Main (string[] args)
{
    while(swapMade == false)
    {

        swapMade = false;
        for (int i = 0; i <= 9; i++)
        {
            compareItems(i);

            if (needSwap == true)
            {
                swapItems(i);
                swapMade = true;
                needSwap = false;
            }
        }
    }
}

static void swapItems(int i;)
{
    string temp;
    array[whatSwap] = array[whatSwap + 1];
    array[whatSwap + 1] = temp;
}

static void compareItems(int i;)
{
    if(array[i] > array[i+1])
    {
        needSwap = true;
    }
}