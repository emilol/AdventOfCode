using System.Collections.Generic;
using System.Linq;

namespace Day6.App
{
    public static class Part2
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

            public IEnumerable<string> ListParents()
            {
                var parent = Parent;
                while (parent != null)
                {
                    yield return parent.Name;
                    parent = parent.Parent;
                }
            }

            public Planet AddParents(IEnumerable<(string Planet, string Satellite)> orbits)
            {
                return AddParents(orbits, this);
            }

            private static Planet AddParents(IEnumerable<(string Planet, string Satellite)> orbits, Planet planet)
            {
                var orbit = orbits.FirstOrDefault(o => o.Satellite == planet.Name);
                if (orbit == default) return planet;

                planet.Parent = new Planet(orbit.Planet).AddParents(orbits);

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
        }
	}
}