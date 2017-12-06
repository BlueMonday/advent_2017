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

    private static int partOne(int[] banks)
    {
        int numRedistributions = 0;
        HashSet<string> seenConfigurations = new HashSet<string>();

        while(true)
        {
            redistribute(banks);
            numRedistributions++;

            string[] stringBanks = Array.ConvertAll(banks, Convert.ToString);
            string configuration = String.Join("", stringBanks);
            if (seenConfigurations.Contains(configuration))
            {
                    return numRedistributions;
            }

            seenConfigurations.Add(configuration);
        }
    }

    private static int partTwo(int[] banks)
    {
        int numRedistributions = 0;
        string duplicateConfiguration = "";
        HashSet<string> seenConfigurations = new HashSet<string>();

        while(true)
        {
            redistribute(banks);
            numRedistributions++;

            string[] stringBanks = Array.ConvertAll(banks, Convert.ToString);
            string configuration = String.Join("", stringBanks);
            if (duplicateConfiguration.Length != 0)
            {
                if (configuration.Equals(duplicateConfiguration))
                {
                    return numRedistributions;
                }
            }
            else
            {
                if (seenConfigurations.Contains(configuration))
                {
                    duplicateConfiguration = configuration;
                    numRedistributions = 0;
                }
                else
                {
                    seenConfigurations.Add(configuration);
                }
            }
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
        Console.WriteLine(partOne(banks));
        Console.WriteLine(partTwo(banks));
        return 0;
    }
}
