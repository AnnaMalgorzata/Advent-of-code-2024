string[] lines = File.ReadAllLines("../../../file.txt");

List<Tuple<int, int>> rules = new List<Tuple<int, int>>();
List<List<int>> sequences = new List<List<int>>();

// Przetwarzanie reguł
int i = 0;
while (i < lines.Length && lines[i].Contains("|"))
{
    var parts = lines[i].Split('|');
    int x = int.Parse(parts[0]);
    int y = int.Parse(parts[1]);
    rules.Add(new Tuple<int, int>(x, y));
    i++;
}

// Ogarnianie sekwencji
for (; i < lines.Length; i++)
{
    if (string.IsNullOrWhiteSpace(lines[i]))
        continue;

    var sequence = lines[i].Split(',').Select(part =>
    {
        int number;
        return int.TryParse(part.Trim(), out number) ? (int?)number : null; // Zwraca null dla nieprawidłowych wartości
    }).Where(num => num.HasValue).Select(num => num.Value).ToList(); // Filtruje null'e

    sequences.Add(sequence);
}

//Part1
var correctSequences = GetCorrectlyOrderedSequences(sequences, rules);
long sumMiddlePageNumbers = SumMiddlePageNumbers(correctSequences);
Console.WriteLine($"Part 1 (from those correctly-ordered): {sumMiddlePageNumbers}");

//Part2
List<List<int>> correctedSequences;
List<List<int>> rightSequences = new List<List<int>>();

do
{
    correctedSequences = GetInCorreclyOrderedSequences(sequences, rules);
    rightSequences.AddRange(GetCorrectlyOrderedSequences(correctedSequences, rules));

} while (correctedSequences.Count > 0);

sumMiddlePageNumbers = SumMiddlePageNumbers(rightSequences);
Console.WriteLine($"Part 2 (after correctly ordering): {sumMiddlePageNumbers}");


static List<List<int>> GetCorrectlyOrderedSequences(List<List<int>> sequences, List<Tuple<int, int>> rules)
{
    List<List<int>> validSequences = new List<List<int>>();
    bool isCorrectlyOrdered = true;
    foreach (var sequence in sequences)
    {
        for (int j = 0; j < sequence.Count - 1; j++)
        {
            int X = sequence[j];
            int Y = sequence[j + 1];
            var foundRule = rules.Find(rule => rule.Item1 == X && rule.Item2 == Y);

            if (foundRule == null)
            {
                isCorrectlyOrdered = false;
                break;
            }
        }
        if (isCorrectlyOrdered)
        {
            validSequences.Add(sequence);
        }
        else
        {
            isCorrectlyOrdered = true;
        }
    }

    return validSequences;
}

static List<List<int>> GetInCorreclyOrderedSequences(List<List<int>> sequences, List<Tuple<int, int>> rules)
{
    List<List<int>> incorrectSequences = new List<List<int>>();
    bool isWrondOrdered = false;
    foreach (var sequence in sequences)
    {
        for (int j = 0; j < sequence.Count - 1; j++)
        {
            int X = sequence[j];
            int Y = sequence[j + 1];
            var foundRule = rules.Find(rule => rule.Item1 == X && rule.Item2 == Y);

            if (foundRule == null)
            {
                var tryAgainRule = rules.Find(rule => rule.Item1 == Y && rule.Item2 == X);
                if(tryAgainRule != null)
                {
                    sequence[j] = Y;
                    sequence[j + 1] = X;
                }
                isWrondOrdered = true;
                break;
            }
        }
        if (isWrondOrdered)
        {
            incorrectSequences.Add(sequence);
            isWrondOrdered = false;
        }
    }
    
    return incorrectSequences;
}

static long SumMiddlePageNumbers(List<List<int>> sequences)
{
    long sum = 0;

    foreach (var sequence in sequences)
    {
        if (sequence.Count > 0)
        {
            int middleIndex = sequence.Count / 2;
            sum += sequence[middleIndex];
        }
    }

    return sum;
}
