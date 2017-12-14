using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

public class DayFourteen
{
    private const int valuesLength = 256;
    private const int numRounds = 64;
    private const int numBlocks = 16;

    const string puzzleInput  = "vbqugkhl";
    const int numRows  = 128;

    // Stolen from stack overflow.
    private static int hexToInt(char hexChar)
    {
        hexChar = char.ToUpper(hexChar);

        return (int)hexChar < (int)'A' ?
            ((int)hexChar - (int)'0') :
            10 + ((int)hexChar - (int)'A');
    }

    private static string knotHash(List<byte> lengths)
    {
        int position = 0;
        int skipSize = 0;
        int[] values = Enumerable.Range(0, valuesLength).ToArray();
        lengths.AddRange(new List<byte>{17, 31, 73, 47, 23});

        for(int j = 0; j < numRounds; j++)
        {
            foreach(int length in lengths)
            {
                if (length > valuesLength)
                {
                    continue;
                }

                for(int i = 0; i < length / 2; i++)
                {
                    int tmp = values[(position + i) % valuesLength];
                    values[(position + i) % valuesLength] = values[(position + length - i - 1) % valuesLength];
                    values[(position + length - i - 1) % valuesLength] = tmp;
                }

                position += (length + skipSize) % valuesLength;
                skipSize += 1;
            }
        }

        List<int> hash = new List<int>();
        for(int i = 0; i < numBlocks; i++)
        {
            int value = values[i * numBlocks];
            for(int j = 1; j < numBlocks; j++)
            {
                value ^= values[(i * numBlocks) + j];
            }
            hash.Add(value);
        }

        StringBuilder hex = new StringBuilder(hash.Count * 2);
        foreach (int value in hash)
        {
            hex.AppendFormat("{0:x2}", value);
        }

        return hex.ToString();
    }

    private static bool[,] generateGrid()
    {
        var grid  = new bool[numRows, numRows];

        for (int i = 0; i < numRows; i++)
        {
            string inputString = puzzleInput + "-" + i;
            List<byte> inputBytes = Encoding.ASCII.GetBytes(inputString).ToList();
            string hash = knotHash(inputBytes);

            var hashArray = hash.ToCharArray();
            for(int j = 0; j < hashArray.Length; j++)
            {
                int intValue = hexToInt(hashArray[j]);

                for (int k = 3; k >= 0; k--)
                {
                    int mask = (int)Math.Pow(2, k);
                    if ((intValue & mask) > 0)
                    {
                        grid[i, (j*4)+(3-k)] = true;
                    }
                }
            }
        }

        return grid;
    }

    private static void visitGroup(int x, int y, HashSet<string> visited, bool[,] grid)
    {
        if (!grid[x,y] || visited.Contains(string.Format("{0},{1}", x, y)))
        {
            return;
        }

        visited.Add(string.Format("{0},{1}", x, y));
        if (x > 0)
        {
            visitGroup(x-1, y, visited, grid);
        }
        if (x < numRows - 1)
        {
            visitGroup(x+1, y, visited, grid);
        }
        if (y > 0)
        {
            visitGroup(x, y-1, visited, grid);
        }
        if (y < numRows - 1)
        {
            visitGroup(x, y+1, visited, grid);
        }
    }

    private static int partOne(string[] lines)
    {
        int used = 0;
        var grid = generateGrid();

        for (int x = 0; x < numRows; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                if (grid[x,y])
                {
                    used++;
                }
            }
        }

        return used;
    }

    private static int partTwo(string[] lines)
    {
        int groups = 0;
        var visited = new HashSet<string>();
        var grid = generateGrid();

        for (int x = 0; x < numRows; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                if (!visited.Contains(string.Format("{0},{1}", x, y)) && grid[x,y])
                {
                    visitGroup(x, y, visited, grid);
                    groups++;
                }
            }
        }

        return groups;
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 14.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        Console.WriteLine(partOne(lines));
        Console.WriteLine(partTwo(lines));
        return 0;
    }
}
