string[] map = File.ReadAllLines("../../../file.txt");

int distinctPositions = CountDistinctPositions(map);
Console.WriteLine($"Distinct positions visited: {distinctPositions}");


static int CountDistinctPositions(string[] map)
{
    // Directions: up, right, down, left
    int[][] directions = new int[][]
    {
            new int[] { -1, 0 }, // Up
            new int[] { 0, 1 },  // Right
            new int[] { 1, 0 },  // Down
            new int[] { 0, -1 }   // Left
    };

    HashSet<(int, int)> visitedPositions = new HashSet<(int, int)>();
    int x = 0, y = 0; // Guard's starting position
    int directionIndex = 0; // Start facing up

    // Find initial position of the guard
    for (int i = 0; i < map.Length; i++)
    {
        for (int j = 0; j < map[i].Length; j++)
        {
            if (map[i][j] == '^')
            {
                x = i;
                y = j;
                break;
            }
        }
    }

    while (true)
    {
        // Mark current position as visited
        visitedPositions.Add((x, y));

        // Calculate next position based on current direction
        int nextX = x + directions[directionIndex][0];
        int nextY = y + directions[directionIndex][1];

        // Check if next position is within bounds
        if (nextX < 0 || nextX >= map.Length || nextY < 0 || nextY >= map[nextX].Length)
            break; // Exit if out of bounds

        if (map[nextX][nextY] == '#') // If there's an obstacle
        {
            // Turn right: change direction
            directionIndex = (directionIndex + 1) % 4;
            continue; // Stay in place and check again after turning
        }

        // Move to the next position
        x = nextX;
        y = nextY;
    }

    return visitedPositions.Count;
}