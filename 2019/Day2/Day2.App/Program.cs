using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Day2.App
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

            var part1 = Part1.Run(input, 12, 2);
            Console.WriteLine($"Part 1: {part1[0]}");

            const int expected = 19690720;
            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    var opcodes = Read(config["InputPath"]);
                    var output = Part2.Run(opcodes, noun, verb);

                    if (output == expected)
                    {
                        Console.WriteLine($"Part 2: noun: {noun} verb: {verb}");
                    }
                }
            }
        }

        public static int[] Read(string inputPath)
        {
            return Parse(File.ReadAllText(inputPath));
        }

        public static int[] Parse(string rawCodes)
        {
            return rawCodes.Split(",").Select(int.Parse).ToArray();
        }
    }
}
