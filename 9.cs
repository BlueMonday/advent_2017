using System;
using System.Collections.Generic;

public class DayNine
{
    private static void process(string input)
    {
        int score = 0;
        Stack<char> stack = new Stack<char>();
        bool inGarbage = false;
        bool ignoreNextChar = false;
        int numGarbage = 0;

        foreach(char c in input)
        {
            if (ignoreNextChar)
            {
                ignoreNextChar = false;
                continue;
            }

            if (c == '!')
            {
                ignoreNextChar = true;
            }
            else if (!inGarbage)
            {
                if (c == '{')
                {
                    stack.Push(c);
                }
                else if (c == '}' && stack.Count > 0)
                {
                    score += stack.Count;
                    stack.Pop();
                }
                else if (c == '<')
                {
                    inGarbage = true;
                }
            }
            else if (c == '>')
            {
                inGarbage = false;
            }
            else
            {
                numGarbage++;
            }

        }

        Console.WriteLine("{0} {1}", score, numGarbage);
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 9.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        process(lines[0]);
        return 0;
    }
}
