using System;
using System.Collections.Generic;

public class DaySixteen
{
    private const int numDances = 1000000000;

    private static void swap(char[] programs, int a, int b)
    {
        char tmp = programs[a];
        programs[a] = programs[b];
        programs[b] = tmp;
    }

    private static string dance(string[] moves, string programsString)
    {
        char[] programs = programsString.ToCharArray();

        foreach(string move in moves)
        {
            switch(move[0])
            {
                case 's':
                    int numPrograms = int.Parse(move.Substring(1, move.Length-1));

                    char[] newPrograms = new char[programs.Length];
                    for (int i = 0; i < numPrograms; i++) {
                        newPrograms[i] = programs[programs.Length - numPrograms + i];
                    }

                    for (int i = 0; i < programs.Length - numPrograms; i++) {
                        newPrograms[numPrograms+i] = programs[i];
                    }

                    programs = newPrograms;
                    break;
                case 'x':
                    string[] parts = move.Substring(1, move.Length-1).Split(new string[]{"/"}, StringSplitOptions.None);
                    int exchangeA = int.Parse(parts[0]);
                    int exchangeB = int.Parse(parts[1]);

                    swap(programs, exchangeA, exchangeB);
                    break;
                case 'p':
                    char partnerA = move[1];
                    char partnerB = move[3];
                    int partnerAPosition = -1;
                    int partnerBPosition = -1;

                    for (int i = 0; i < programs.Length; i++)
                    {
                        char c = programs[i];

                        if (c == partnerA)
                        {
                            partnerAPosition = i;
                            if (partnerBPosition != -1)
                            {
                                break;
                            }

                        }
                        else if (c == partnerB)
                        {
                            partnerBPosition = i;
                            if (partnerAPosition != -1)
                            {
                                break;
                            }
                        }
                    }

                    swap(programs, partnerAPosition, partnerBPosition);
                    break;
            }
        }
        return new string(programs);
    }

    private static string partOne(string[] moves)
    {
        string programsString = "abcdefghijklmnop";
        return dance(moves, programsString);
    }

    private static string partTwo(string[] moves)
    {
        string programsString = "abcdefghijklmnop";
        var danceResults = new Dictionary<string, string>();

        int i = 0;
        while(true)
        {
            if (danceResults.ContainsKey(programsString))
            {
                break;
            }

            string result = dance(moves, programsString);
            danceResults[programsString] = result;
            programsString = result;

            i++;
        }

        int loopLength = i;
        while (i < numDances)
        {
            if (i < numDances - loopLength) {
                i += loopLength;
            }
            else
            {
                programsString = danceResults[programsString];
                i++;
            }
        }

        return programsString;
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 16.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);
        string[] moves = lines[0].Split(new string[]{","}, StringSplitOptions.None);

        Console.WriteLine(partOne(moves));
        Console.WriteLine(partTwo(moves));
        return 0;
    }
}
