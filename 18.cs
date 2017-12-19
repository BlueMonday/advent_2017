using System;
using System.Collections.Generic;

public class DayEighteen
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

    private static long partOne(string[] instructions)
    {
        var registers = new Dictionary<string, long>();
        long currInstruction = 0;
        long lastSound = -1;

        while(true)
        {
            string instruction = instructions[currInstruction];
            string[] parts = instruction.Split(null);
            switch (parts[0])
            {
                case "snd":
                    long sndValue = getValue(registers, parts[1]);
                    lastSound = sndValue;
                    break;
                case "set":
                    long setValue = getValue(registers, parts[2]);
                    if (!registers.ContainsKey(parts[1]))
                    {
                        registers[parts[1]] = 0;
                    }
                    registers[parts[1]] = setValue;
                    break;
                case "add":
                    long addValue = getValue(registers, parts[2]);
                    if (!registers.ContainsKey(parts[1]))
                    {
                        registers[parts[1]] = 0;
                    }
                    registers[parts[1]] += addValue;
                    break;
                case "mul":
                    long mulValue = getValue(registers, parts[2]);
                    if (!registers.ContainsKey(parts[1]))
                    {
                        registers[parts[1]] = 0;
                    }
                    registers[parts[1]] *= mulValue;
                    break;
                case "mod":
                    long modValue = getValue(registers, parts[2]);
                    if (!registers.ContainsKey(parts[1]))
                    {
                        registers[parts[1]] = 0;
                    }
                    registers[parts[1]] %= modValue;
                    break;
                case "rcv":
                    long rcvValue = getValue(registers, parts[1]);
                    if (rcvValue != 0)
                    {
                        return lastSound;
                    }
                    break;
                case "jgz":
                    long jgzValue = getValue(registers, parts[1]);
                    long offset = getValue(registers, parts[2]);
                    if (jgzValue > 0)
                    {
                        currInstruction += offset;
                        continue;
                    }
                    break;
            }

            currInstruction++;
            if (currInstruction < 0 || currInstruction >= instructions.Length)
            {
                break;
            }
        }

        return lastSound;
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 18.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        Console.WriteLine(partOne(lines));
        return 0;
    }
}
