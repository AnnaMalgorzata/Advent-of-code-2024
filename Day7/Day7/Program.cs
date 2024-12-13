string filePath = "../../../file.txt"; 
string[] lines = File.ReadAllLines(filePath);
long totalCalibrationResult = 0;

foreach (var line in lines)
{
    var parts = line.Split(':');
    long testValue = long.Parse(parts[0].Trim());
    long[] numbers = Array.ConvertAll(parts[1].Trim().Split(' '), long.Parse);

    //Console.WriteLine(testValue + " " + numbers);

    if (CanMakeTestValue(testValue, numbers))
    {
        totalCalibrationResult += testValue;
    }
}

Console.WriteLine($"Suma wartości testowych: {totalCalibrationResult}");

static bool CanMakeTestValue(long target, long[] numbers)
{
    int n = numbers.Length;

    for (int i = 0; i < (1 << (n - 1)); i++)
    {
        long result = numbers[0];
        for (int j = 0; j < n - 1; j++)
        {
            // Ustalanie operatora na podstawie bitów
            if ((i & (1 << j)) != 0) // Bit ustawiony - operator *
            {
                result *= numbers[j + 1];
            }
            else // Bit nie ustawiony - operator +
            {
                result += numbers[j + 1];
            }
        }

        if (result == target)
        {
            return true;
        }
    }

    return false;
}
