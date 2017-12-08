using System;
using System.Collections.Generic;

public class DaySix
{
    private static void redistribute(int[] banks)
    {
        int index = -1;
        int largest = int.MinValue;

        for (int i = 0; i < banks.Length; i++)
        {
            if (banks[i] > largest)
            {
                index = i;
                largest = banks[i];
            }
        }
        banks[index] = 0;

        while(largest > 0)
        {
            index = (index + 1) % banks.Length;
            banks[index] += 1;
            largest--;
        }
    }

    private static int numberOfRedistributionsUntilDuplicate(int[] banks)
    {
        int numRedistributions = 0;
        HashSet<string> seenConfigurations = new HashSet<string>();

        while(true)
        {
            string[] stringBanks = Array.ConvertAll(banks, Convert.ToString);
            string configuration = String.Join("", stringBanks);
            if (seenConfigurations.Contains(configuration))
            {
                    return numRedistributions;
            }

            seenConfigurations.Add(configuration);

            redistribute(banks);
            numRedistributions++;
        }
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 6.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        string[] stringBanks = lines[0].Split(null);
        int[] banks = Array.ConvertAll(stringBanks, int.Parse);
        Console.WriteLine(numberOfRedistributionsUntilDuplicate(banks));
        Console.WriteLine(numberOfRedistributionsUntilDuplicate(banks));
        return 0;
    }
}
