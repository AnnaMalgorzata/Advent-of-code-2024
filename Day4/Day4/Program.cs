

string filePath = "../../../file.txt";
char[,] grid = LoadGridFromFile(filePath);
string word = "XMAS";
int count = CountOccurrences(grid, word);
Console.WriteLine($"Liczba wystąpień słowa '{word}': {count}");

    static char[,] LoadGridFromFile(string filePath)
{
    string[] lines = File.ReadAllLines(filePath);
    int rows = lines.Length;
    int cols = lines[0].Length;
    char[,] grid = new char[rows, cols];

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            grid[i, j] = lines[i][j];
        }
    }

    return grid;
}

static int CountOccurrences(char[,] grid, string word)
{
    int count = 0;
    int rows = grid.GetLength(0);
    int cols = grid.GetLength(1);

    for (int row = 0; row < rows; row++)
    {
        for (int col = 0; col < cols; col++)
        {
            // Sprawdzenie wszystkich kierunków dla XMAS:
            count += SearchInAllDirections(grid, word, row, col);

            //Part2:
            //count += CheckForXMas(grid, row, col);
        }
    }

    return count;
}
static int CheckForXMas(char[,] grid, int row, int col)
{
    int occurrences = 0;

    // Sprawdzenie wzoru M.A.S
    if (grid[row - 1, col] == 'M' && // Górne M
        grid[row + 1, col] == 'M' && // Dolne M
        grid[row, col] == 'A' &&     // Środkowe A
        grid[row, col - 1] == 'S' && // Lewe S
        grid[row, col + 1] == 'S')   // Prawe S
    {
        occurrences++; // Znaleziono X-MAS
    }

    // Sprawdzenie wzoru M.M.A.S.S (inny wariant)
    if (grid[row - 1, col] == 'M' && // Górne M
        grid[row + 1, col] == 'S' && // Dolne S
        grid[row, col] == 'A' &&      // Środkowe A
        grid[row, col - 1] == 'M' &&  // Lewe M
        grid[row, col + 1] == 'S')    // Prawe S
    {
        occurrences++; // Znaleziono X-MAS w tej formie
    }

    // Sprawdzenie wzoru S.S.A.M.M (odwrócone)
    if (grid[row - 1, col] == 'S' && // Górne S
        grid[row + 1, col] == 'S' && // Dolne S
        grid[row, col] == 'A' &&      // Środkowe A
        grid[row, col - 1] == 'M' &&  // Lewe M
        grid[row, col + 1] == 'M')    // Prawe M
    {
        occurrences++; // Znaleziono X-MAS w tej formie odwróconej
    }

    return occurrences; // Zwraca całkowitą liczbę wystąpień znalezionych w tej pozycji
}

static int SearchInAllDirections(char[,] grid, string word, int startRow, int startCol)
{
    int count = 0;

    // Definicja kierunków: (deltaRow, deltaCol)
    (int, int)[] directions = {
            (0, 1), // prawo
            (0, -1), //lewo
            (-1, 0), //góra
            (1, 0), //dół
            (1, 1), // Dół-Prawo
            (-1, -1), // Góra-Lewo
            (1, -1), // Dół-Lewo
            (-1, 1) // Góra-Prawo
        };

    foreach (var direction in directions)
    {
            if (Search(grid, word, startRow, startCol, direction.Item1, direction.Item2))
            {
                count++;
            }
    }

    return count;
}

static bool Search(char[,] grid, string word, int row, int col, int deltaRow, int deltaCol)
{
    for (int i = 0; i < word.Length; i++)
    {
        int newRow = row + i * deltaRow;
        int newCol = col + i * deltaCol;

        // Sprawdzenie granic planszy
        if (newRow < 0 || newRow >= grid.GetLength(0) || newCol < 0 || newCol >= grid.GetLength(1))
            return false;

        if (grid[newRow, newCol] != word[i])
            return false;
    }

    return true;
}