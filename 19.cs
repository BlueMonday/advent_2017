using System;
using System.Collections.Generic;

public class DayNineteen
{
    enum Direction{North, East, South, West};

    private static void followDiagram(string[] diagram)
    {
        int steps = 0;
        int x = 0;
        int y = 0;
        var direction = Direction.South;
        var letters = new List<char>();

        for (int i = 0; i < diagram[0].Length; i++)
        {
            if(diagram[0][i] == '|')
            {
                x = i;
                break;
            }
        }

        while (true)
        {
            char c = diagram[y][x];
            if (Char.IsLetter(c))
            {
                letters.Add(c);
            }
            else if (c == '+')
            {
                if (direction == Direction.South || direction == Direction.North)
                {
                    if (x + 1 < diagram[0].Length && diagram[y][x+1] != ' ')
                    {
                        direction = Direction.East;
                    }
                    else if (x - 1 > 0 && diagram[y][x-1] != ' ')
                    {
                        direction = Direction.West;
                    }
                }
                else
                {
                    // Console.WriteLine(diagram[y-1][x]);
                    if (y + 1 < diagram.Length && diagram[y+1][x] != ' ')
                    {
                        direction = Direction.South;
                    }
                    else if (y - 1 >= 0 && diagram[y-1][x] != ' ')
                    {
                        direction = Direction.North;
                    }
                }
            }
            else if (c == ' ')
            {
                break;
            }

            switch (direction)
            {
                case Direction.North:
                    y--;
                    break;
                case Direction.East:
                    x++;
                    break;
                case Direction.South:
                    y++;
                    break;
                case Direction.West:
                    x--;
                    break;
            }
            steps++;
        }

        Console.WriteLine("{0} {1}", new string(letters.ToArray()), steps);
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 19.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        followDiagram(lines);
        return 0;
    }
}
