using System;
using System.Collections.Generic;

public class DayTwelve
{
    private static Dictionary<int, HashSet<int>> parseNeighbors(string[] lines)
    {
        var neighbors = new Dictionary<int, HashSet<int>>();

        foreach(string line in lines)
        {
            string[] parts = line.Split(new string[]{"<->"}, StringSplitOptions.None);
            int programID = int.Parse(parts[0]);
            if(!neighbors.ContainsKey(programID))
            {
                neighbors.Add(programID, new HashSet<int>());
            }

            int[] connectedProgramIDs = Array.ConvertAll(parts[1].Split(','), int.Parse);
            foreach(int connectedProgramID in connectedProgramIDs)
            {
                neighbors[programID].Add(connectedProgramID);

                if(!neighbors.ContainsKey(connectedProgramID))
                {
                    neighbors.Add(connectedProgramID, new HashSet<int>{programID});
                }
                else
                {
                    neighbors[connectedProgramID].Add(programID);
                }
            }
        }

        return neighbors;
    }

    private static void visitConnected(int programID, HashSet<int> visited, Dictionary<int, HashSet<int>> neighbors)
    {
        visited.Add(programID);
        foreach(int connectedProgram in neighbors[programID])
        {
            if(visited.Contains(connectedProgram))
            {
                continue;
            }

            visited.Add(connectedProgram);
            visitConnected(connectedProgram, visited, neighbors);
        }
    }

    private static int partOne(string[] lines)
    {
        var neighbors = parseNeighbors(lines);
        var visited = new HashSet<int>{};
        visitConnected(0, visited, neighbors);
        return visited.Count;
    }

    private static int partTwo(string[] lines)
    {
        var neighbors = parseNeighbors(lines);

        int groups = 0;
        var visited = new HashSet<int>{};
        foreach(KeyValuePair<int, HashSet<int>> entry in neighbors)
        {
            if (!visited.Contains(entry.Key))
            {
                visitConnected(entry.Key, visited, neighbors);
                groups++;
            }
        }
        return groups;
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 12.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        Console.WriteLine(partOne(lines));
        Console.WriteLine(partTwo(lines));
        return 0;
    }
}
