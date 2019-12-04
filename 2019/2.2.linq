<Query Kind="Program">
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var expected = 19690720;
	for (int noun = 0; noun < 100; noun++)
	{
		for (int verb = 0; verb < 100; verb++)
		{
			var opcodes = Parse(ReadOpCodes());
			var output = Run(InitializeMemory(noun, verb, opcodes));

			if (output == expected) {
				noun.Dump();
				verb.Dump();
			}
		}
	};
}

int Run(int[] memory)
{
	var instructionPointer = 0;
	var instruction = Instruction.Continue;
	
	while (instruction != Instruction.Halt)
	{
		instruction = memory[instructionPointer];

		var parameters = new[] { memory[memory[instructionPointer + 1]], memory[memory[instructionPointer + 2]] };
		var output = memory[instructionPointer + 3];
		

		if (instruction == Instruction.Add)
		{
			memory[output] = parameters[0] + parameters[1];
		}
		else if (instruction == Instruction.Multiply)
		{
			memory[output] = parameters[0] * parameters[1];
		}
		else
		{
			throw new InvalidOperationException("Unrecognized opcode");
		}

		instructionPointer = instructionPointer + 4;
		instruction = memory[instructionPointer];
	}

	return memory[0];
}

int[] InitializeMemory(int noun, int verb, int[] opCodes)
{
	opCodes[1] = noun;
	opCodes[2] = verb;

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

static class Instruction
{
	public static int Continue = 0;
	public static int Add = 1;
	public static int Multiply = 2;
	public static int Halt = 99;
}