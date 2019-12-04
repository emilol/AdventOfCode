<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	Run(ReadWires(@"R75,D30,R83,U83,L12,D49,R71,U7,L72
U62,R66,U55,R34,D71,R55,D58,R83")).Dump();
	Run(ReadWires(@"R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51
U98,R91,D20,R16,D67,R40,U7,R15,U6,R7")).Dump();
	Run(ReadWires()).Dump();
}

int Run(IEnumerable<string[]> wireInstructions)
{
	var positions = wireInstructions
		.Select(w => Positions(w)
			.Where(p => p.dist > 0)
			.OrderBy(p => p.dist));

	var intersections = positions.First().Intersect(positions.Last(), new PositionComparer());

	return intersections.First().dist;
}

IEnumerable<(int x, int y, int dist)> Positions(string[] instructions)
{
	var origin = (x: 0, y: 0, dist: 0); 
	var previousPosition = origin;
	
	yield return origin;
	
	foreach (var instruction in instructions)
	{
		var direction = instruction[0].ToString();
		var distance = int.Parse(instruction.Substring(1));

		for (int i = 1; i <= distance; i++)
		{
			var position = previousPosition;
			
			if (direction == "R") 
			{
				position.x = previousPosition.x + 1;
				position.dist = Math.Abs(position.x) + Math.Abs(position.y);
				
				yield return position;
				
				previousPosition = position;
				continue;
			}
			
			if (direction == "L")
			{
				position.x = previousPosition.x - 1;
				position.dist = Math.Abs(position.x) + Math.Abs(position.y);
				
				yield return position;
				
				previousPosition = position;
				continue;
			}
			
			if (direction == "U")
			{
				position.y = previousPosition.y + 1;
				position.dist = Math.Abs(position.x) + Math.Abs(position.y);
				
				yield return position;
				
				previousPosition = position;
				continue;
			}
			
			if (direction == "D")
			{
				position.y = previousPosition.y - 1;
				position.dist = Math.Abs(position.x) + Math.Abs(position.y);
				
				yield return position;
				
				previousPosition = position;
				continue;
			}
		}
	}
}

IEnumerable<string[]> ReadWires()
{
	var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.3.txt");
	using (var stream = File.OpenRead(inputPath))
	using (var reader = new StreamReader(stream))
	{
		string wire;
		while ((wire = reader.ReadLine()) != null)
		{
			var codes = Parse(wire);
			yield return codes;
		}
	}
}

IEnumerable<string[]> ReadWires(string blah)
{
	using (var reader = new StringReader(blah))
	{
		string wire;
		while ((wire = reader.ReadLine()) != null)
		{
			var codes = Parse(wire);
			yield return codes;
		}
	}
}

string[] Parse(string input) 
{
	return input.Split(",").ToArray();
}

public class PositionComparer : System.Collections.Generic.IEqualityComparer<(int x, int y, int dist)>
{
	public bool Equals((int x, int y, int dist) a, (int x, int y, int dist) b) {
		return a.x == b.x && a.y == b.y;
	}
	
	public int GetHashCode((int x, int y, int dist) a)
	{
		int hCode = a.x ^ a.y;
		return hCode.GetHashCode();
	}
}