using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Day1.App
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

            var total = Part1.TotalFuel(Read(config["InputPath"]));
            var totalIncludingFuelWeight = Part2.FuelForModules(Read(config["InputPath"]));

            Console.WriteLine($"Part 1: {total}");
            Console.WriteLine($"Part 2: {totalIncludingFuelWeight}");
		}

        public static string[] Read(string inputPath)
        {
            return File.ReadAllLines(inputPath);
        }
    }
}
