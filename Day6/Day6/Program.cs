using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string[] input = File.ReadAllLines("../../../file.txt"); // Wczytanie mapy z pliku
        int result = PredictGuardPath(input);
        Console.WriteLine($"Strażnik odwiedził {result} różnych pozycji.");
    }

    static int PredictGuardPath(string[] input)
    {
        int rows = input.Length;
        int cols = input[0].Length;

        // Tworzenie mapy
        char[,] map = new char[rows, cols];
        (int x, int y) start = (-1, -1);
        int direction = 0; // 0 = up, 1 = right, 2 = down, 3 = left

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                map[i, j] = input[i][j];
                if ("^>v<".Contains(map[i, j]))
                {
                    start = (i, j);
                    direction = "^>v<".IndexOf(map[i, j]);
                    map[i, j] = '.'; // Zastąpienie pozycji strażnika pustym miejscem
                }
            }
        }

        // Ruchy: góra, prawo, dół, lewo
        (int dx, int dy)[] directions = { (-1, 0), (0, 1), (1, 0), (0, -1) };

        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        (int x, int y) position = start;
        visited.Add(position);

        // Maksymalna liczba kroków jako zabezpieczenie przed nieskończoną pętlą
        int maxSteps = rows * cols * 4; // Heurystycznie wybrana wartość
        int steps = 0;

        while (steps < maxSteps)
        {
            steps++;

            // Obliczenie nowej pozycji w obecnym kierunku
            (int nx, int ny) = (position.x + directions[direction].dx, position.y + directions[direction].dy);

            // Sprawdzenie, czy wychodzi poza mapę
            if (nx < 0 || nx >= rows || ny < 0 || ny >= cols)
                break;

            // Sprawdzenie, czy ruch jest zablokowany
            if (map[nx, ny] == '#')
            {
                // Obrót w prawo
                direction = (direction + 1) % 4;
                Console.WriteLine($"direction: {direction}");
            }
            else
            {
                // Ruch naprzód
                position = (nx, ny);
                visited.Add(position);
                Console.WriteLine($"Visited: {visited.Count}");
            }
        }

        return visited.Count;
    }
}
