using System.Collections.Generic;
using System.Linq;

namespace Day1.App
{
    public static class Part1
    {
        public static int Fuel(int mass)
        {
            return mass / 3 - 2;
        }

        public static int TotalFuel(IEnumerable<string> modules)
        {
            return modules.Sum(module => Fuel(int.Parse(module)));
        }
    }
}