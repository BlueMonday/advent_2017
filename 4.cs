using System;
using System.Collections.Generic;

public class DayFour
{
    private static bool validPassphrase(string passphrase) {
            HashSet<string> usedWords = new HashSet<string>();

            string[] words = passphrase.Split(null);
            foreach(string word in words)
            {
                if (usedWords.Contains(word))
                {
                    return false;
                }

                usedWords.Add(word);
            }

            return true;
    }

    private static int partOne(string[] passphrases)
    {
        int valid = 0;
        foreach(string passphrase in passphrases)
        {
            if (validPassphrase(passphrase))
            {
                valid++;
            }
        }
        return valid;
    }

    private static bool validPassphraseAmended(string passphrase) {
            List<HashSet<char>> usedCharSets = new List<HashSet<char>>();

            string[] words = passphrase.Split(null);
            foreach(string word in words)
            {
                HashSet<char> charSet = new HashSet<char>(word.ToCharArray());

                foreach(HashSet<char> usedCharSet in usedCharSets)
                {
                    if (usedCharSet.SetEquals(charSet))
                    {
                        return false;
                    }
                }

                usedCharSets.Add(charSet);
            }

            return true;
    }

    private static int partTwo(string [] passphrases)
    {
        int valid = 0;
        foreach(string passphrase in passphrases)
        {
            if (validPassphraseAmended(passphrase))
            {
                valid++;
            }
        }
        return valid;
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 4.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        Console.WriteLine(partOne(lines));
        Console.WriteLine(partTwo(lines));
        return 0;
    }
}
