using System;
using System.Text;
using System.Collections.Generic;

public class DayTwentyOne
{
    private static string reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse( charArray );
        return new string( charArray );
    }

    private static string rotate(string art)
    {
        string[] rows = art.Split(new string[]{"/"}, StringSplitOptions.RemoveEmptyEntries);
        char[,] newRows = new char[rows.Length, rows.Length];

        for (int i = 0; i < rows.Length; i++)
        {
            for (int j = 0; j < rows[0].Length; j++)
            {
                 newRows[rows.Length - j - 1, i] = rows[i][j];
            }
        }


        StringBuilder s = new StringBuilder();
        for (int i = 0; i < newRows.GetLength(0); i++)
        {
            for (int j = 0; j < newRows.GetLength(1); j++)
            {
                s.Append(newRows[i,j]);
            }

            if (i != newRows.GetLength(0) - 1)
            {
                s.Append("/");
            }
        }

        return s.ToString();
    }

    private static string flipHorizontal(string art)
    {
        string[] rows = art.Split(new string[]{"/"}, StringSplitOptions.None);
        string[] flipped = new string[rows.Length];
        for (int i = 0; i < rows.Length; i++)
        {
            flipped[i] = reverse(rows[i]);
        }
        return String.Join("/", flipped);
    }

    private static string flipVertical(string art)
    {
            string[] rows = art.Split(new string[]{"/"}, StringSplitOptions.None);
            Array.Reverse(rows);
            return String.Join("/", rows);
    }

    private static int enhance(string[] rulesDefinitions, int numIterations)
    {
        Dictionary<string, List<List<bool>>> rules = new Dictionary<string, List<List<bool>>>();

        foreach(string rulesDefinition in rulesDefinitions)
        {
            string[] parts = rulesDefinition.Split(new string[]{" => "}, StringSplitOptions.None);
            string[] enhancementRows = parts[1].Split(new string[]{"/"}, StringSplitOptions.None);

            var enhancement = new List<List<bool>>();
            foreach (string enhancementRow in enhancementRows)
            {
                var row = new List<bool>();
                foreach(char c in enhancementRow)
                {
                    if (c == '#')
                    {
                        row.Add(true);
                    }
                    else
                    {
                        row.Add(false);
                    }
                }
                enhancement.Add(row);
            }

            var input = parts[0];
            rules[input] = enhancement;
            rules[flipHorizontal(input)] = enhancement;
            rules[flipVertical(input)] = enhancement;

            for (int i = 0; i < 3; i++)
            {
                var rotated = rotate(input);
                rules[rotated] = enhancement;
                rules[flipHorizontal(rotated)] = enhancement;
                rules[flipVertical(rotated)] = enhancement;

                input = rotated;
            }
        }

        var grid = new List<List<bool>>();
        grid.Add(new List<bool>{false, true, false});
        grid.Add(new List<bool>{false, false, true});
        grid.Add(new List<bool>{true, true, true});

        for (int i = 0; i < numIterations; i++)
        {
            var newGrid = new List<List<bool>>();

            if (grid.Count % 2 == 0)
            {
                for(int j = 0; j < grid.Count/2; j++)
                {
                    for(int k = 0; k < grid[j].Count/2; k++)
                    {
                        StringBuilder subGrid = new StringBuilder();
                        for(int l = 0; l < 2; l++)
                        {
                            for(int m = 0; m < 2; m++)
                            {
                                if (grid[l+(j*2)][m+(k*2)])
                                {
                                    subGrid.Append("#");
                                }
                                else
                                {
                                    subGrid.Append(".");
                                }
                            }

                            if (l != 1)
                            {
                                subGrid.Append("/");
                            }
                        }

                        var enhancement = rules[subGrid.ToString()];
                        if (k == 0)
                        {
                            newGrid.Add(new List<bool>(enhancement[0]));
                            newGrid.Add(new List<bool>(enhancement[1]));
                            newGrid.Add(new List<bool>(enhancement[2]));
                        }
                        else
                        {
                            newGrid[0+(j*3)].AddRange(enhancement[0]);
                            newGrid[1+(j*3)].AddRange(enhancement[1]);
                            newGrid[2+(j*3)].AddRange(enhancement[2]);
                        }
                    }
                }
            }
            else if (grid.Count % 3 == 0)
            {
                for(int j = 0; j < grid.Count/3; j++)
                {
                    for(int k = 0; k < grid[0].Count/3; k++)
                    {
                        StringBuilder subGrid = new StringBuilder();
                        for(int l = 0; l < 3; l++)
                        {
                            for(int m = 0; m < 3; m++)
                            {
                                if (grid[l+(j*3)][m+(k*3)])
                                {
                                    subGrid.Append("#");
                                }
                                else
                                {
                                    subGrid.Append(".");
                                }
                            }

                            if (l != 2)
                            {
                                subGrid.Append("/");
                            }
                        }

                        var enhancement = rules[subGrid.ToString()];
                        if (k == 0)
                        {
                            newGrid.Add(new List<bool>(enhancement[0]));
                            newGrid.Add(new List<bool>(enhancement[1]));
                            newGrid.Add(new List<bool>(enhancement[2]));
                            newGrid.Add(new List<bool>(enhancement[3]));
                        }
                        else
                        {
                            newGrid[0+(j*4)].AddRange(enhancement[0]);
                            newGrid[1+(j*4)].AddRange(enhancement[1]);
                            newGrid[2+(j*4)].AddRange(enhancement[2]);
                            newGrid[3+(j*4)].AddRange(enhancement[3]);
                        }
                    }
                }
            }

            grid = newGrid;
        }

        int numOn = 0;
        foreach (List<bool> row in grid)
        {
            foreach (bool pixel in row)
            {
                if (pixel)
                {
                    numOn++;
                }
            }
        }
        return numOn;
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 21.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        Console.WriteLine(enhance(lines, 5));
        Console.WriteLine(enhance(lines, 18));
        return 0;
    }
}
