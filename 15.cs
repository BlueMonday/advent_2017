using System;

public class DayFifteen
{
    private const long factorA = 16807;
    private const long factorB = 48271;
    private const long divisor = 2147483647;
    private const long numPairs = 40000000;
    private const long numPairs2 = 5000000;
    private const long mask = 0xffff;

    private const int initialValueA = 116;
    private const int initialValueB = 299;

    private static int partOne()
    {
        int matches = 0;
        long valueA = initialValueA;
        long valueB = initialValueB;

        for (int i = 0; i < numPairs; i++)
        {
            valueA *= factorA;
            valueA %= divisor;

            valueB *= factorB;
            valueB %= divisor;

            if ((valueA & mask) == (valueB & mask))
            {
                matches++;
            }
        }

        return matches;
    }

    private static long nextValueA(long previous)
    {
        long next = previous;

        while(true)
        {
            next *= factorA;
            next %= divisor;

            if (next % 4 == 0)
            {
                return next;
            }
        }
    }

    private static long nextValueB(long previous)
    {
        long next = previous;

        while(true)
        {
            next *= factorB;
            next %= divisor;

            if (next % 8 == 0)
            {
                return next;
            }

        }
    }

    private static int partTwo()
    {
        int matches = 0;
        long valueA = initialValueA;
        long valueB = initialValueB;

        for (int i = 0; i < numPairs2; i++)
        {
            valueA = nextValueA(valueA);
            valueB = nextValueB(valueB);

            if ((valueA & mask) == (valueB & mask))
            {
                matches++;
            }
        }

        return matches;
    }

    public static int Main(string[] args)
    {
        Console.WriteLine(partOne());
        Console.WriteLine(partTwo());
        return 0;
    }
}
