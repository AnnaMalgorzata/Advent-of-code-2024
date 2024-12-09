using System;
using System.Collections.Generic;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main()
    {
        List<int> left = new List<int>();
        List<int> right = new List<int>();

        string filePath = "../../../file.txt";
        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Podział linii na części
                var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    // Dodawanie wartości do odpowiednich list
                    if (int.TryParse(parts[0], out int leftValue))
                    {
                        left.Add(leftValue);
                    }
                    if (int.TryParse(parts[1], out int rightValue))
                    {
                        right.Add(rightValue);
                    }
                }
            }
        }

        right.Sort();
        left.Sort();

        long sum = 0;

        // Sprawdzanie, ile razy każda liczba z lewej listy pojawia się w prawej liście
        foreach (var number in left)
        {
            int count = right.Count(x => x == number);
            sum += count*number;
        }

        Console.WriteLine(sum);

        // Wyświetlenie wyników -> punkt 1
        //Console.WriteLine("Left List:");
        //for(int i = 0; i < right.Count(); i++)
        //{
        //    sum += Math.Abs(right.ElementAt(i) - left.ElementAt(i));
        //}

        //Console.WriteLine(sum);
    }
}