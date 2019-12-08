using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Day4.App
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

            var part1 = Part1.Run();
            var part2 = Part2.Run();

            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
		}

        public static string[] Read(string inputPath)
        {
            return File.ReadAllLines(inputPath);
        }
    }
}
