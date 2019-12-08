using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Day5.App
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

            var part1 = Part1.Run(Read(config["InputPath"]).ToArray(), 1);
            var part2 = Part2.Run(Read(config["InputPath"]).ToArray(), 5);

            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
        }

        public static int[] Read(string inputPath)
        {
            return Parse(File.ReadAllText(inputPath));
        }

        static int[] Parse(string input)
        {
            return input.Split(",").Select(int.Parse).ToArray();
        }
    }
}
