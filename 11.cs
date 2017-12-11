using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

public class DayEleven
{
    private static int distance(string path, bool maxEver = false)
    {
        int x = 0;
        int y = 0;
        int z = 0;
        int distance = 0;

        string[] steps = path.Split(',');
        foreach(string step in steps)
        {
            switch(step)
            {
                case "n":
                    z--;
                    y++;
                    break;
                case "s":
                    z++;
                    y--;
                    break;
                case "ne":
                    x++;
                    z--;
                    break;
                case "se":
                    x++;
                    y--;
                    break;
                case "sw":
                    x--;
                    z++;
                    break;
                case "nw":
                    y++;
                    x--;
                    break;
            }

            int currentDistance = (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2;
            if (maxEver)
            {
                if (currentDistance > distance)
                {
                    distance = currentDistance;
                }
            }
            else
            {
                distance = currentDistance;
            }
        }

        return distance;
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 11.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        Console.WriteLine(distance(lines[0]));
        Console.WriteLine(distance(lines[0], maxEver: true));
        return 0;
    }
}
