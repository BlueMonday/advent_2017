using System;
using System.Collections.Generic;

public class DayThirteen
{
    private static int partOne(string[] lines)
    {
        int severity = 0;
        int maxDepth = 0;

        var depthToRange = new Dictionary<int, int>();

        foreach(string line in lines)
        {
            string[] parts = line.Split(new string[]{": "}, StringSplitOptions.None);
            int depth = int.Parse(parts[0]);
            int range = int.Parse(parts[1]);

            depthToRange.Add(depth, range);
            if (depth > maxDepth)
            {
                maxDepth = depth;
            }
        }

        int currentDepth = -1;
        while(currentDepth < maxDepth)
        {
            currentDepth++;
            if (depthToRange.ContainsKey(currentDepth))
            {
                int position = currentDepth % ((depthToRange[currentDepth] * 2) - 2);
                if (position > (depthToRange[currentDepth] - 1))
                {
                    position = depthToRange[currentDepth] - (position - depthToRange[currentDepth] - 1);
                }

                if (position == 0)
                {
                    severity += currentDepth * depthToRange[currentDepth];
                }
            }
        }

        return severity;
    }

    private static bool caught(Dictionary<int, int> depthToRange, int maxDepth, int delay)
    {
        int currentDepth = -1;
        while(currentDepth < maxDepth)
        {
            currentDepth++;

            if (depthToRange.ContainsKey(currentDepth))
            {
                int position = (delay + currentDepth) % ((depthToRange[currentDepth] * 2) - 2);
                if (position > (depthToRange[currentDepth] - 1))
                {
                    position = depthToRange[currentDepth] - (position - depthToRange[currentDepth] - 1);
                }

                if (position == 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static int partTwo(string[] lines)
    {
        int delay = 0;
        int maxDepth = 0;
        var depthToRange = new Dictionary<int, int>();

        foreach(string line in lines)
        {
            string[] parts = line.Split(new string[]{": "}, StringSplitOptions.None);
            int depth = int.Parse(parts[0]);
            int range = int.Parse(parts[1]);

            depthToRange.Add(depth, range);
            if (depth > maxDepth)
            {
                maxDepth = depth;
            }
        }

        while(true)
        {
            if (!caught(depthToRange, maxDepth, delay))
            {
                return delay;
            }

            delay++;
        }
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 13.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        Console.WriteLine(partOne(lines));
        Console.WriteLine(partTwo(lines));
        return 0;
    }
}
