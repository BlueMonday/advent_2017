using System;
using System.Collections.Generic;

public struct Component
{
    public int a;
    public int b;

    public Component(int a, int b)
    {
        this.a = a;
        this.b = b;
    }
}

public struct Result
{
    public int length;
    public int strength;

    public Result(int length, int strength)
    {
        this.length = length;
        this.strength = strength;
    }
}


public class DayTwentyFour
{
    private static List<Component> parseComponents(string[] lines)
    {
        var components = new List<Component>();
        foreach (string line in lines)
        {
            var parts = line.Split('/');
            components.Add(new Component(int.Parse(parts[0]), int.Parse(parts[1])));
        }
        return components;
    }

    private static int strongestBridge(int v, List<Component> components)
    {
        if (components.Count == 0)
        {
            return 0;
        }

        int strongest = 0;
        foreach(var component in components)
        {
            if (component.a == v)
            {
                var newComponents = new List<Component>(components);
                newComponents.Remove(component);

                int strength = component.a + component.b + strongestBridge(component.b, newComponents);
                if (strength > strongest)
                {
                    strongest = strength;
                }
            }
            else if (component.b == v)
            {
                var newComponents = new List<Component>(components);
                newComponents.Remove(component);

                int strength = component.a + component.b + strongestBridge(component.a, newComponents);
                if (strength > strongest)
                {
                    strongest = strength;
                }
            }
        }

        return strongest;
    }

    private static Result longestStrongestBridge(int v, List<Component> components)
    {
        if (components.Count == 0)
        {
            return new Result(0, 0);
        }

        var bestResult = new Result(0, 0);
        foreach(var component in components)
        {
            if (component.a == v)
            {
                var newComponents = new List<Component>(components);
                newComponents.Remove(component);

                var result = longestStrongestBridge(component.b, newComponents);
                result.length += 1;
                result.strength += component.a + component.b;
                if (result.length > bestResult.length)
                {
                    bestResult = result;
                }
                else if (result.length == bestResult.length && result.strength > bestResult.strength)
                {
                    bestResult = result;
                }
            }
            else if (component.b == v)
            {
                var newComponents = new List<Component>(components);
                newComponents.Remove(component);

                var result = longestStrongestBridge(component.a, newComponents);
                result.length += 1;
                result.strength += component.a + component.b;
                if (result.length > bestResult.length)
                {
                    bestResult = result;
                }
                else if (result.length == bestResult.length && result.strength > bestResult.strength)
                {
                    bestResult = result;
                }
            }
        }

        return bestResult;
    }

    private static int partOne(string[] lines)
    {
        var components = parseComponents(lines);
        return strongestBridge(0, components);
    }

    private static int partTwo(string[] lines)
    {
        var components = parseComponents(lines);
        var bestResult = longestStrongestBridge(0, components);
        return bestResult.strength;
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 24.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        Console.WriteLine(partOne(lines));
        Console.WriteLine(partTwo(lines));
        return 0;
    }
}
