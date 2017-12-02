using System;

public class DayOne
{
    private static int partOne(string[] lines)
    {
        int checksum = 0;

        foreach (string line in lines)
        {
            int largest = int.MinValue;
            int smallest = int.MaxValue;

            string[] values = line.Split(null);
            foreach (string value in values)
            {
                int parsedValue =  int.Parse(value);
                if (parsedValue > largest)
                {
                    largest = parsedValue;
                }

                if (parsedValue < smallest)
                {
                    smallest = parsedValue;
                }
            }

            checksum += largest - smallest;
        }

        return checksum;
    }

    private static int partTwo(string[] lines)
    {
        int checksum = 0;

        foreach (string line in lines)
        {
            string[] stringValues = line.Split(null);
            int[] values = Array.ConvertAll(stringValues, int.Parse);
            Array.Sort<int>(values);

            for (int i = 0; i < values.Length; i++)
            {
                int divisor = values[i];

                ArraySegment<int> dividends = new ArraySegment<int>(values, i + 1, values.Length - i - 1);
                foreach(int dividend in dividends)
                {
                    if (dividend % divisor == 0)
                    {
                        Console.WriteLine("{0} {1}", dividend, divisor);
                        checksum += dividend / divisor;
                        break;
                    }
                }
            }
        }

        return checksum;
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 2.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        Console.WriteLine(partOne(lines));
        Console.WriteLine(partTwo(lines));
        return 0;
    }
}
