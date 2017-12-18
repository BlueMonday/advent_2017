using System;
using System.Collections.Generic;

public class DaySeventeen
{
    private const int numValues = 2018;
    private const long numValues2 = 50000001;
    private const int stepsForward = 314;

    private static int partOne()
    {
        var circularBuffer = new LinkedList<int>(new List<int>{0});
        var node = circularBuffer.First;

        for (int i = 1; i < numValues; i++)
        {
            for (int j = 0; j < stepsForward; j++)
            {
                if (node.Next == null)
                {
                    node = circularBuffer.First;
                } else {
                    node = node.Next;
                }
            }

            circularBuffer.AddAfter(node, i);
            node = node.Next;
        }

        return node.Next.Value;
    }

    private static long partTwo()
    {
        var circularBuffer = new LinkedList<long>(new List<long>{0});
        var node = circularBuffer.First;

        for (long i = 1; i < numValues2; i++)
        {
            for (int j = 0; j < stepsForward; j++)
            {
                if (node.Next == null)
                {
                    node = circularBuffer.First;
                } else {
                    node = node.Next;
                }
            }

            circularBuffer.AddAfter(node, i);
            node = node.Next;

            if (i % 100000 == 0)
            {
                Console.WriteLine(i);
            }
        }

        return circularBuffer.First.Next.Value;
    }

    public static int Main(string[] args)
    {
        Console.WriteLine(partOne());
        Console.WriteLine(partTwo());
        return 0;
    }
}
