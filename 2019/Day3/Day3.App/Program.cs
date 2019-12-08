using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Day3.App
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

            var part1 = Part1.Run(Read(config["InputPath"]));
            var part2 = Part2.Run(Read(config["InputPath"]));

            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
		}

        public static IEnumerable<string[]> Read(string inputPath)
        {
            return File.ReadAllLines(inputPath).Select(Parse);
        }

        static string[] Parse(string input)
        {
            return input.Split(",").ToArray();
        }
    }
}
