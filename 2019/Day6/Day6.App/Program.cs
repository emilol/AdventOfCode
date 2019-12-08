using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Day6.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .AddCommandLine(args)
                .Build();

            var input = Read(config["InputPath"]).ToArray();
            var com = new Part1.Planet("COM").AddSatellites(input);

            Console.WriteLine($"Part 1: {com.TotalOrbits()}");

            var you = new Part2.Planet("YOU").AddParents(input).ListParents().ToList();
            var san = new Part2.Planet("SAN").AddParents(input).ListParents().ToList();

            var commonOrbit = you.Intersect(san).First();
            var totalJumps = you.IndexOf(commonOrbit) + san.IndexOf(commonOrbit);

            Console.WriteLine($"Part 2: {totalJumps}");
		}

        public static IEnumerable<(string Planet, string Satellite)> Read(string inputPath)
        {
            return File.ReadAllLines(inputPath)
                .Select(Parse);
        }

        public static (string Planet, string Satellite) Parse(string orbit)
        {
            var thing = orbit.Split(")");
            return (Planet: thing[0], Satellite: thing[1]);
        }
    }
}
