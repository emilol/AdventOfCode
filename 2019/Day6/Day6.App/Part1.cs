using System.Collections.Generic;
using System.Linq;

namespace Day6.App
{
    public static class Part1
	{
        public class Planet
        {
            public string Name { get; set; }
            public Planet Parent { get; set; }
            public PlanetList Satellites { get; set; }

            public Planet(string name)
            {
                Name = name;
                Satellites = new PlanetList(this);
            }

            public Planet(string name, Planet parent)
            {
                Name = name;
                Satellites = new PlanetList(this);
                Parent = parent;
            }

            public int OrbitCount()
            {
                if (Parent == null) return 0;
                else return (1 + Parent.OrbitCount());
            }

            public int TotalOrbits()
            {
                return OrbitCount() + Satellites.Sum(s => s.TotalOrbits());
            }

            public Planet AddSatellites(IEnumerable<(string Planet, string Satellite)> orbits)
            {
                return AddSatellites(orbits, this);
            }

            private static Planet AddSatellites(IEnumerable<(string Planet, string Satellite)> orbits, Planet planet)
            {
                var satellites = orbits.Where(o => o.Planet == planet.Name)
                    .Select(o => new Planet(o.Satellite).AddSatellites(orbits));

                foreach (var satellite in satellites)
                {
                    planet.Satellites.Add(satellite);
                }

                return planet;
            }
        }

        public class PlanetList : List<Planet>
        {
            public Planet Parent;

            public PlanetList(Planet Parent)
            {
                this.Parent = Parent;
            }

            public new Planet Add(Planet planet)
            {
                base.Add(planet);
                planet.Parent = Parent;
                return planet;
            }
        }
    }
}