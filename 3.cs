using System;
using System.Collections.Generic;

public class DayThree
{
    enum Direction{North, East, South, West};

    private const int puzzleInput = 289326;

    private static int lengthOfSquareSide()
    {
        int lengthOfSide = 1;

        while ((lengthOfSide * lengthOfSide) < puzzleInput)
        {
            lengthOfSide += 2;
        }

        return lengthOfSide;
    }


    private static int partOne()
    {
        int squareLength = lengthOfSquareSide();
        int lastNumber = squareLength * squareLength;

        int closestCorner = lastNumber;

        while (closestCorner - puzzleInput >= squareLength)
        {
            closestCorner -= squareLength - 1;
        }

        int distanceToCenterOfSide = Math.Abs(puzzleInput - (closestCorner - (squareLength / 2)));
        return (squareLength / 2) + distanceToCenterOfSide;
    }

    private static int partTwo()
    {
        Dictionary<int, Dictionary<int, int>> grid = new Dictionary<int, Dictionary<int, int>>();

        Direction direction = Direction.East;
        int x = 0;
        int y = 0;
        int value = 1;

        int currentSquareLength = 1;
        while (value < puzzleInput)
        {
            if (!grid.ContainsKey(x))
            {
                grid.Add(x, new Dictionary<int, int>());
            }

            int newValue = 0;
            if (grid.ContainsKey(x + 1))
            {
                if (grid[x+1].ContainsKey(y))
                {
                    newValue += grid[x+1][y];
                }
                if (grid[x+1].ContainsKey(y + 1))
                {
                    newValue += grid[x+1][y+1];
                }
                if (grid[x+1].ContainsKey(y - 1))
                {
                    newValue += grid[x+1][y-1];
                }
            }

            if (grid.ContainsKey(x - 1))
            {
                if (grid[x-1].ContainsKey(y))
                {
                    newValue += grid[x-1][y];
                }
                if (grid[x-1].ContainsKey(y + 1))
                {
                    newValue += grid[x-1][y+1];
                }
                if (grid[x-1].ContainsKey(y - 1))
                {
                    newValue += grid[x-1][y-1];
                }
            }

            if (grid[x].ContainsKey(y + 1))
            {
                    newValue += grid[x][y+1];
            }

            if (grid[x].ContainsKey(y - 1))
            {
                    newValue += grid[x][y-1];
            }

            if (newValue == 0)
            {
                newValue = 1;
            }
            value = newValue;
            grid[x].Add(y, value);

            if (direction == Direction.North)
            {
                if (y == (currentSquareLength / 2))
                {
                    x -= 1;
                    direction = Direction.West;
                }
                else
                {
                    y += 1;
                }
            }
            else if (direction == Direction.East)
            {
                if (x == (currentSquareLength / 2))
                {
                    y += 1;
                    direction = Direction.North;
                    currentSquareLength += 2;
                }
                else
                {
                    x += 1;
                }
            }
            else if (direction == Direction.South)
            {
                if (y == (-1 * (currentSquareLength / 2)))
                {
                    x += 1;
                    direction = Direction.East;
                }
                else
                {
                    y -= 1;
                }
            }
            else if (direction == Direction.West)
            {
                if (x == (-1 * (currentSquareLength / 2)))
                {
                    y -= 1;
                    direction = Direction.South;
                }
                else
                {
                    x -= 1;
                }
            }
        }

        return value;
    }

    public static void Main()
    {
        Console.WriteLine(partOne());
        Console.WriteLine(partTwo());
    }
}
