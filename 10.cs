using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

public class DayTen
{
    private const int valuesLength = 256;
    private const int numRounds = 64;
    private const int numBlocks = 16;

    private static int partOne(int[] lengths)
    {
        int position = 0;
        int skipSize = 0;
        int[] values = new int[valuesLength];
        for (int i = 0; i < valuesLength; i++)
        {
            values[i] = i;
        }

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

        return values[0] * values[1];
    }

    private static string partTwo(List<byte> lengths)
    {
        int position = 0;
        int skipSize = 0;
        int[] values = Enumerable.Range(0, valuesLength).ToArray();

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

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 10.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        int [] lengths = Array.ConvertAll(lines[0].Split(','), int.Parse);
        Console.WriteLine(partOne(lengths));

        byte[] byteLengths = Encoding.ASCII.GetBytes(lines[0]);
        List<byte> lengthsList = new List<byte>(byteLengths);
        lengthsList.AddRange(new List<byte>{17, 31, 73, 47, 23});
        Console.WriteLine(partTwo(lengthsList));
        return 0;
    }
}
