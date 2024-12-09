using System;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath = "../../../file.txt";
        using (var reader = new StreamReader(filePath))
        {
            string line;
            int safes = 0;

            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int[] numbers = Array.ConvertAll(parts, int.Parse);

                // Sprawdź, czy ciąg jest bezpieczny lub można go uczynić bezpiecznym przez usunięcie jednego poziomu
                if (IsSafe(numbers) || CanBecomeSafeByRemovingOneLevel(numbers))
                {
                    safes++;
                }
            }

            Console.WriteLine(safes);
        }
    }

    static bool IsSafe(int[] numbers)
    {
        return IsIncreasing(numbers) || IsDecreasing(numbers);
    }

    static bool CanBecomeSafeByRemovingOneLevel(int[] numbers)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            // Create a new array without the current level
            int[] modifiedNumbers = numbers.Where((_, index) => index != i).ToArray();

            // Check if the modified array is safe
            if (IsSafe(modifiedNumbers))
            {
                return true; // It's possible to make it safe
            }
        }
        return false; // No single removal made it safe
    }

    static bool IsIncreasing(int[] numbers)
    {
        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] <= numbers[i - 1])
            {
                return false;
            }
            if (numbers[i] - numbers[i-1] < 1 || numbers[i] - numbers[i - 1] > 3)
            {
                return false;
            }
        }
        return true;
    }

    static bool IsDecreasing(int[] numbers)
    {
        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] >= numbers[i - 1])
            {
                return false;
            }
            if (numbers[i-1] - numbers[i] < 1 || numbers[i - 1] - numbers[i] > 3)
            {
                return false;
            }
        }
        return true;
    }
}