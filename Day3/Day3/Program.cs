// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

string filePath = "../../../file.txt";
var reader = new StreamReader(filePath);
var line = reader.ReadToEnd();
reader.Close();
string pattern = @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)";
MatchCollection matches = Regex.Matches(line, pattern);

int totalSum = 0;
bool isActive = true; // Stan aktywacji dla instrukcji mul

foreach (Match match in matches)
{
    if (match.Groups[0].Value.StartsWith("mul"))
    {
        if (isActive) // Sprawdź, czy instrukcja jest aktywna
        {
            int x = int.Parse(match.Groups[1].Value);
            int y = int.Parse(match.Groups[2].Value);
            totalSum += x * y; // Dodaj wynik mnożenia do sumy
        }
    }
    else if (match.Groups[0].Value == "do()")
    {
        isActive = true; // Aktywuj przyszłe instrukcje mul
    }
    else if (match.Groups[0].Value == "don't()")
    {
        isActive = false; // Dezaktywuj przyszłe instrukcje mul
    }
}

Console.WriteLine("Suma wyników mnożenia: " + totalSum);