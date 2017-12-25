using System;
using System.Collections.Generic;

public class DayTwentyFive
{
    private static int numSteps = 12173597;

    private static int partOne()
    {
        int state = 0;
        var tape = new LinkedList<int>(new int[]{0});
        var curr = tape.First;

        for (int i = 0; i < numSteps; i++)
        {
            switch(state)
            {
                case 0:
                    if (curr.Value == 0)
                    {
                        curr.Value = 1;
                        if (curr.Next == null)
                        {
                            tape.AddAfter(curr, 0);
                        }
                        curr = curr.Next;
                        state = 1;
                    }
                    else if (curr.Value == 1)
                    {
                        curr.Value = 0;
                        if (curr.Previous == null)
                        {
                            tape.AddBefore(curr, 0);
                        }
                        curr = curr.Previous;
                        state = 2;
                    }
                    break;
                case 1:
                    if (curr.Value == 0)
                    {
                        curr.Value = 1;
                        if (curr.Previous == null)
                        {
                            tape.AddBefore(curr, 0);
                        }
                        curr = curr.Previous;
                        state = 0;
                    }
                    else if (curr.Value == 1)
                    {
                        curr.Value = 1;
                        if (curr.Next == null)
                        {
                            tape.AddAfter(curr, 0);
                        }
                        curr = curr.Next;
                        state = 3;
                    }
                    break;
                case 2:
                    if (curr.Value == 0)
                    {
                        curr.Value = 1;
                        if (curr.Next == null)
                        {
                            tape.AddAfter(curr, 0);
                        }
                        curr = curr.Next;
                        state = 0;
                    }
                    else if (curr.Value == 1)
                    {
                        curr.Value = 0;
                        if (curr.Previous == null)
                        {
                            tape.AddBefore(curr, 0);
                        }
                        curr = curr.Previous;
                        state = 4;
                    }
                    break;
                case 3:
                    if (curr.Value == 0)
                    {
                        curr.Value = 1;
                        if (curr.Next == null)
                        {
                            tape.AddAfter(curr, 0);
                        }
                        curr = curr.Next;
                        state = 0;
                    }
                    else if (curr.Value == 1)
                    {
                        curr.Value = 0;
                        if (curr.Next == null)
                        {
                            tape.AddAfter(curr, 0);
                        }
                        curr = curr.Next;
                        state = 1;
                    }
                    break;
                case 4:
                    if (curr.Value == 0)
                    {
                        curr.Value = 1;
                        if (curr.Previous == null)
                        {
                            tape.AddBefore(curr, 0);
                        }
                        curr = curr.Previous;
                        state = 5;
                    }
                    else if (curr.Value == 1)
                    {
                        curr.Value = 1;
                        if (curr.Previous == null)
                        {
                            tape.AddBefore(curr, 0);
                        }
                        curr = curr.Previous;
                        state = 2;
                    }
                    break;
                case 5:
                    if (curr.Value == 0)
                    {
                        curr.Value = 1;
                        if (curr.Next == null)
                        {
                            tape.AddAfter(curr, 0);
                        }
                        curr = curr.Next;
                        state = 3;
                    }
                    else if (curr.Value == 1)
                    {
                        curr.Value = 1;
                        if (curr.Next == null)
                        {
                            tape.AddAfter(curr, 0);
                        }
                        curr = curr.Next;
                        state = 0;
                    }
                    break;
            }
        }

        int count = 0;
        curr = tape.First;
        while(curr != null)
        {
            if (curr.Value == 1)
            {
                count++;
            }
            curr = curr.Next;
        }

        return count;
    }

    public static int Main(string[] args)
    {
        Console.WriteLine(partOne());
        return 0;
    }
}
