using System;
using System.Collections.Generic;

namespace Day1.App
{
    public static class Part2
    {
        public static int Fuel(int mass)
        {
            return Math.Max((int)(mass / 3) - 2, 0);
        }

        public static int TotalFuel(int fuelMass)
        {
            var fuelForCarryingFuel = Fuel(fuelMass);
            fuelMass += fuelForCarryingFuel;

            while (fuelForCarryingFuel > 0)
            {
                fuelForCarryingFuel = Fuel(fuelForCarryingFuel);
                fuelMass += fuelForCarryingFuel;
            }

            return fuelMass;
        }

        public static int FuelForModules(IEnumerable<string> modules)
        {
            var total = 0;
            foreach (var module in modules)
            {
                var fuel = Fuel(int.Parse(module));
                total += TotalFuel(fuel);
            }

            return total;
        }
	}
}