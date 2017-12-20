using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Particle
{
    public long vx;
    public long vy;
    public long vz;
    public long ax;
    public long ay;
    public long az;
    public long x;
    public long y;
    public long z;

    public Particle(long x, long y, long z, long vx, long vy, long vz, long ax, long ay, long az)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.vx = vx;
        this.vy = vy;
        this.vz = vz;
        this.ax = ax;
        this.ay = ay;
        this.az = az;
    }

    public void Tick() {
        this.vx += this.ax;
        this.vy += this.ay;
        this.vz += this.az;
        this.x += this.vx;
        this.y += this.vy;
        this.z += this.vz;
    }
}

public class DayTwenty
{
    private const int numIterations = 1000;

    private static List<Particle> parseParticles(string[] definitions)
    {
        var particles = new List<Particle>();
        foreach(string definition in definitions)
        {
            string[] parts = definition.Split(null);
            Regex definitionRegex = new Regex(@"[pva]=<(?<x>-?\d+),(?<y>-?\d+),(?<z>-?\d+)>,?");
            Match positionMatch = definitionRegex.Match(parts[0]);
            Match velocityMatch = definitionRegex.Match(parts[1]);
            Match accelerationMatch = definitionRegex.Match(parts[2]);

            Particle particle = new Particle(
                long.Parse(positionMatch.Groups["x"].ToString()),
                long.Parse(positionMatch.Groups["y"].ToString()),
                long.Parse(positionMatch.Groups["z"].ToString()),
                long.Parse(velocityMatch.Groups["x"].ToString()),
                long.Parse(velocityMatch.Groups["y"].ToString()),
                long.Parse(velocityMatch.Groups["z"].ToString()),
                long.Parse(accelerationMatch.Groups["x"].ToString()),
                long.Parse(accelerationMatch.Groups["y"].ToString()),
                long.Parse(accelerationMatch.Groups["z"].ToString()));
            particles.Add(particle);
        }

        return particles;
    }

    private static int partOne(string[] lines)
    {
        var particles = parseParticles(lines);

        for(int i = 0; i < numIterations; i++)
        {
            foreach(Particle particle in particles)
            {
                particle.Tick();
            }
        }

        int closest = 0;
        long minDistance = long.MaxValue;

        for(int i = 0; i < particles.Count; i++)
        {
            Particle particle = particles[i];
            long distance = Math.Abs(particle.x) + Math.Abs(particle.y) + Math.Abs(particle.z);
            if (distance < minDistance) {
                closest = i;
                minDistance = distance;
            }
        }

        return closest;
    }

    private static int partTwo(string[] lines)
    {
        var particles = parseParticles(lines);

        for(int i = 0; i < numIterations; i++)
        {
            Dictionary<string, int> positions = new Dictionary<string, int>();
            foreach(Particle particle in particles)
            {
                particle.Tick();

                string position = string.Format("{0},{1},{2}", particle.x, particle.y, particle.z);
                if (positions.ContainsKey(position))
                {
                    positions[position] += 1;
                }
                else
                {
                    positions[position] = 1;
                }
            }

            List<Particle> newParticles = new List<Particle>();
            foreach(Particle particle in particles)
            {
                string position = string.Format("{0},{1},{2}", particle.x, particle.y, particle.z);
                if (positions[position] == 1)
                {
                    newParticles.Add(particle);
                }
            }
            particles = newParticles;
        }

        return particles.Count;
    }

    public static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: 20.exe <input_file>");
            return -1;
        }

        string inputFile = args[0];
        string[] lines = System.IO.File.ReadAllLines(inputFile);

        Console.WriteLine(partOne(lines));
        Console.WriteLine(partTwo(lines));
        return 0;
    }
}
