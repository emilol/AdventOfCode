<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var total = 0;

	Fuel(12).Dump();
	Fuel(14).Dump();
	Fuel(1969).Dump();
	Fuel(100756).Dump();

	var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.1.txt");
	using (var stream = File.OpenRead(inputPath))
	using (var reader = new StreamReader(stream))
	{
		string line;
		while ((line = reader.ReadLine()) != null)
		{
			var fuel = Fuel(int.Parse(line));
			total += fuel;
		}
	}
	
	total.Dump();
}

// Define other methods, classes and namespaces here
int Fuel(int mass) {
	return (int)(mass/3) - 2;
}