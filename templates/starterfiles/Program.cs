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

            var input = Read(config["InputPath"]);

            Console.WriteLine($"Part 1: ");
            Console.WriteLine($"Part 2: ");
		}

        public static string[] Read(string inputPath)
        {
            return File.ReadAllLines(inputPath);
        }
    }
}
