using System;
using System.Collections.Generic;

public class DayTwentyThree
{
    private static long getValue(Dictionary<string, long> registers, string valueOrRegister)
    {
        long value = 0;
        bool isNumeric = long.TryParse(valueOrRegister, out value);
        if (!isNumeric)
        {
            if (registers.ContainsKey(valueOrRegister))
            {
                value = registers[valueOrRegister];
            }
            else
            {
                value = 0;
                registers[valueOrRegister] = value;
            }
        }

        return value;
    }

    private static long partOne(string[] lines)
    {
        int numMul = 0;
        long i = 0;
        var registers = new Dictionary<string, long>()
            {
                {"a", 0},
                {"b", 0},
                {"c", 0},
                {"d", 0},
                {"e", 0},
                {"f", 0},
                {"g", 0},
                {"h", 0},
            };

        while(true)
        {
            if (i < 0 || i >= lines.Length) {
                break;
            }

            var instruction = lines[i];
            var parts = instruction.Split(null);
            switch(parts[0])
            {
                case "set":
                    registers[parts[1]] = getValue(registers, parts[2]);
                    break;
                case "sub":
                    registers[parts[1]] -= getValue(registers, parts[2]);
                    break;
                case "mul":
                    registers[parts[1]] *= getValue(registers, parts[2]);
                    numMul++;
                    break;
                case "jnz":
                    var condition = getValue(registers, parts[1]);
                    if (condition != 0)
                    {
                        i += getValue(registers, parts[2]);
                        continue;
                    }
                    break;
            }

            i++;
        }

        return numMul;
    }

    private static int partTwo(string[] lines)
    {
        int b = 0;
        int c = 0;
        int h = 0;

        b = 81 * 100 + 100000;
        c = b + 17000;

        while (true)
        {
            for (int i = 2; i * i < b; i++)
            {
                if (b % i == 0)
                {
                    h += 1;
                    break;
                }
            }

            if (b - c == 0)
            {
                break;
            }

            b += 17;
        }

        return h;
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 23.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        Console.WriteLine(partOne(lines));
        Console.WriteLine(partTwo(lines));
        return 0;
    }
}
