using System;

public class DayFive
{
    private static int runInstructions(string[] instructions, bool stranger = false)
    {
        int[] jumps = Array.ConvertAll(instructions, int.Parse);
        int numberOfSteps = 0;
        int currentIndex = 0;

        while(currentIndex <= jumps.Length - 1 && currentIndex >= 0)
        {
            int nextIndex = currentIndex + jumps[currentIndex];
            if (stranger && jumps[currentIndex] >= 3)
            {
                jumps[currentIndex] -= 1;
            }
            else
            {
                jumps[currentIndex] += 1;
            }
            currentIndex = nextIndex;
            numberOfSteps++;
        }

        return numberOfSteps;
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 5.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        Console.WriteLine(runInstructions(lines));
        Console.WriteLine(runInstructions(lines, stranger: true));
        return 0;
    }
}
