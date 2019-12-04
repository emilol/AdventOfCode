<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	Run(Parse("1,0,0,0,99")).Dump();
	Run(Parse("2,3,0,3,99")).Dump();
	Run(Parse("2,4,4,5,99,0")).Dump();
	Run(Parse("1,1,1,4,99,5,6,0,99")).Dump();
	Run(InitializeOpCodes(12, 2, Parse(ReadOpCodes())))[0].Dump();
}

int[] Run(int[] opcodes)
{
	var index = 0;
	var op = Op.Continue;

	while (op != Op.Halt)
	{
		op = opcodes[index];
		
		var args = new[] { opcodes[opcodes[index + 1]], opcodes[opcodes[index + 2]] };
		var output = opcodes[index + 3];
		
		if (op == Op.Add)
		{
			opcodes[output] = args[0] + args[1];
		} else if (op == Op.Multiply)
		{
			opcodes[output] = args[0] * args[1];
		}
		else
		{
			throw new InvalidOperationException("Unrecognized opcode");
		}

		index = index + 4;
		op = opcodes[index];
	}
	
	return opcodes;
}


int[] InitializeOpCodes(int a, int b, int[] opCodes)
{
	opCodes[1] = a;
	opCodes[2] = b;

	return opCodes;
}

string ReadOpCodes()
{
	var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.2.txt");
	using (var stream = File.OpenRead(inputPath))
	using (var reader = new StreamReader(stream))
	{
		return reader.ReadToEnd();
	}
}

int[] Parse(string rawCodes)
{
	return rawCodes.Split(",").Select(o => int.Parse(o)).ToArray();
}

static class Op
{
	public static int Continue = 0;
    public static int Add = 1;
    public static int Multiply = 2;
    public static int Halt = 99;
}