using System;
using System.Collections.Generic;

public class DayEight
{
    private static void doOperation(Dictionary<string, int> registers, string register, string operation, int amount)
    {
        if (operation == "inc")
        {
            registers[register] += amount;
        }
        else if (operation == "dec")
        {
            registers[register] -= amount;
        }
    }

    private static int largest(string[] lines, bool largestEver = false)
    {
        Dictionary<string, int> registers = new Dictionary<string, int>();
        int largest = 0;

        foreach(string line in lines)
        {
            string[] parts = line.Split(null);

            string register = parts[0];
            string operation = parts[1];
            int amount = int.Parse(parts[2]);
            string conditionRegister = parts[4];
            string conditionOperator = parts[5];
            int conditionValue = int.Parse(parts[6]);

            if(!registers.ContainsKey(register))
            {
                registers.Add(register, 0);
            }

            int value;
            if (registers.ContainsKey(conditionRegister))
            {
                value = registers[conditionRegister];
            }
            else
            {
                value = 0;
            }

            switch(conditionOperator)
            {
                case ">":
                    if (value > conditionValue)
                    {
                        doOperation(registers, register, operation, amount);
                    }
                    break;
                case "<":
                    if (value < conditionValue)
                    {
                        doOperation(registers, register, operation, amount);
                    }
                    break;
                case "==":
                    if (value == conditionValue)
                    {
                        doOperation(registers, register, operation, amount);
                    }
                    break;
                case ">=":
                    if (value >= conditionValue)
                    {
                        doOperation(registers, register, operation, amount);
                    }
                    break;
                case "<=":
                    if (value <= conditionValue)
                    {
                        doOperation(registers, register, operation, amount);
                    }
                    break;
                case "!=":
                    if (value != conditionValue)
                    {
                        doOperation(registers, register, operation, amount);
                    }
                    break;
            }

            if (largestEver)
            {
                foreach(KeyValuePair<string, int> entry in registers)
                {
                    if (entry.Value > largest)
                    {
                        largest = entry.Value;
                    }
                }
            }
        }

        if (!largestEver)
        {
            foreach(KeyValuePair<string, int> entry in registers)
            {
                if (entry.Value > largest)
                {
                    largest = entry.Value;
                }
            }
        }

        return largest;
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 8.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        Console.WriteLine(largest(lines));
        Console.WriteLine(largest(lines, largestEver: true));
        return 0;
    }
}
