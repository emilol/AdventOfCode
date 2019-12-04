<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{

	TotalFuel(Fuel(14)).Dump();
	TotalFuel(Fuel(1969)).Dump();
	TotalFuel(Fuel(100756)).Dump();

	var total = FuelForModules();
	
	total.Dump();
}

int TotalFuel(int fuelMass)
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

int FuelForModules()
{
	var total = 0;

	var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.1.txt");
	using (var stream = File.OpenRead(inputPath))
	using (var reader = new StreamReader(stream))
	{
		string line;
		while ((line = reader.ReadLine()) != null)
		{
			var fuel = Fuel(int.Parse(line));
			total += TotalFuel(fuel);
		}
	}
	
	return total;
}

// Define other methods, classes and namespaces here
int Fuel(int mass) {
	return Math.Max((int)(mass/3) - 2, 0);
}