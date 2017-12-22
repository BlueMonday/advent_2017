using System;
using System.Collections.Generic;

public class DayTwentyTwo
{
    private const int numBursts = 10000;
    private const int numBursts2 =10000000;

    private static int mod(int x, int m) {
        int r = x%m;
        return r<0 ? r+m : r;
    }

    private static int partOne(string[] lines)
    {
        var grid = new Dictionary<string, bool>();

        for(int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            for(int j = 0; j < line.Length; j++)
            {
                string coordinate = string.Format("{0},{1}", j, -1 * i);
                if (line[j] == '#')
                {
                    grid[coordinate] = true;
                }
                else
                {
                    grid[coordinate] = false;
                }
            }
        }

        int numInfections = 0;
        int x = lines[0].Length / 2;
        int y = (-1 * lines.Length) / 2;
        int direction = 0;
        for(int i = 0; i < numBursts; i++)
        {
            string coordinate = string.Format("{0},{1}", x, y);
            if (!grid.ContainsKey(coordinate))
            {
                grid[coordinate] = false;
            }

            if (grid[coordinate])
            {
                direction = mod(direction + 1, 4);
                grid[coordinate] = false;
            }
            else
            {
                direction = mod(direction - 1, 4);
                grid[coordinate] = true;
                numInfections++;
            }

            switch(direction)
            {
                case 0:
                    y++;
                    break;
                case 1:
                    x++;
                    break;
                case 2:
                    y--;
                    break;
                case 3:
                    x--;
                    break;
            }

        }

        return numInfections;
    }

    private static int partTwo(string[] lines)
    {
        var grid = new Dictionary<string, int>();

        for(int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            for(int j = 0; j < line.Length; j++)
            {
                string coordinate = string.Format("{0},{1}", j, -1 * i);
                if (line[j] == '#')
                {
                    grid[coordinate] = 2;
                }
                else
                {
                    grid[coordinate] = 0;
                }
            }
        }

        int numInfections = 0;
        int x = lines[0].Length / 2;
        int y = (-1 * lines.Length) / 2;
        int direction = 0;
        for(int i = 0; i < numBursts2; i++)
        {
            string coordinate = string.Format("{0},{1}", x, y);
            if (!grid.ContainsKey(coordinate))
            {
                grid[coordinate] = 0;
            }

            switch(grid[coordinate])
            {
                case 0:
                    direction = mod(direction - 1, 4);
                    grid[coordinate] = 1;
                    break;
                case 1:
                    grid[coordinate] = 2;
                    numInfections++;
                    break;
                case 2:
                    direction = mod(direction + 1, 4);
                    grid[coordinate] = 3;
                    break;
                case 3:
                    direction = mod(direction + 2, 4);
                    grid[coordinate] = 0;
                    break;
            }

            switch(direction)
            {
                case 0:
                    y++;
                    break;
                case 1:
                    x++;
                    break;
                case 2:
                    y--;
                    break;
                case 3:
                    x--;
                    break;
            }

        }

        return numInfections;
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 22.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        Console.WriteLine(partOne(lines));
        Console.WriteLine(partTwo(lines));
        return 0;
    }
}
