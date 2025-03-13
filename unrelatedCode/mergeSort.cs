public static int[] mergeSort(int[] array)
{
    int[] left;
    int[] right;
    int[] result = new int[array.Length];
    //As this is a recursive algorithm, we need to have a base case to 
    //avoid an infinite recustion and therefore a stackoverflow
    if (array.Length <= 1)
    {
        return array;
    }
    //The exact midpoint of our array
    int midPoint = array.Length / 2;
    //Will represent our "left" array
    left = new int[midPoint];

    //if array has an even number of elements, the left and right array will have the same number of
    //elements
    if(array.Length % 2 == 0)
    {
        right = new int[midPoint + 1];
    } //if array has an odd number of elements, the right array will have one more element that left
    else 
    {
        right = new int[midPoint + 1];
    }
    //populate left array
    for (int i = 0; i < midPoint; i++)
    {
        left[i] = array[i];
    }
    //populate right array
    int x = 0;
    //We start our index from the midpoint, as we have already populated the left array from 0 to midpoint
    for (int i = midPoint; i < array.Length; i++)
    {
        right[x] = array[i];
        x++;
    }
    //Recursively sort the left array
    left = mergeSort(left);
    //Recursively sort the right array
    right = mergeSort(right);
    //Merge our two sorted arrays
    result = merge(left, right);
    return result;
}