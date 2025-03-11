        static void DisplayNumbersAllowed(List<int> NumbersAllowed)
        {
            Console.Write("Numbers available: ");
            foreach (int Number in NumbersAllowed)
            {
                Console.Write($"{Number}  ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }