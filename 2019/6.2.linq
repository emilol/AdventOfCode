<Query Kind="Program" />

void Main()
{
	var orbits = ReadOrbits();
	
	var you = new Planet("YOU").AddParents(orbits).ListParents().ToList();
	var san = new Planet("SAN").AddParents(orbits).ListParents().ToList();

	var commonOrbit = you.Intersect(san).First();
	int totalJumps = you.IndexOf(commonOrbit) + san.IndexOf(commonOrbit);
	
	totalJumps.Dump();
}

class Planet
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

class PlanetList : List<Planet>
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

	public Planet Add(string name)
	{
		return Add(new Planet(name));
	}
}

(string Planet, string Satellite) Parse(string orbit) {
	var thing = orbit.Split(")");
	return (Planet: thing[0], Satellite: thing[1]);
}

IEnumerable<(string Planet, string Satellite)> ReadOrbits()
{
	var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.6.txt");
	return File.ReadAllLines(inputPath)
		.Select(o => Parse(o));
}