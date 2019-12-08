using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Day7.App.Part1;
using Day7.App.Part2;
using Microsoft.Extensions.Configuration;

namespace Day7.App
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

            var input = Read(config["InputPath"]);

            var part1 = input.Part1(Permutate(new[] { 0, 1, 2, 3, 4 }));
            var part2 = input.Part2(Permutate(new[] { 5, 6, 7, 8, 9 }));

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

        private static IEnumerable<int[]> Permutate(int[] source)
        {
            if (source.Length == 1) return new List<int[]> { source };

            var permutations = from c in source
                from p in Permutate(source.Where(x => x != c).ToArray())
                select p.Prepend(c).ToArray();

            return permutations;
        }

    }
}
