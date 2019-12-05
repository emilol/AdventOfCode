<Query Kind="Program" />

void Main()
{
	var opcodes = Parse(ReadOpCodes());
	var output = Run(opcodes, 1).Dump();
}

delegate void Operation(int[] memory, ref int pointer, int opCode, int input, ref int? output);

int? Run(int[] memory, int programInput)
{
	var operations = new Dictionary<int, Operation>();

	operations[1] = Add;
	operations[2] = Multiply;
	operations[3] = Input;
	operations[4] = Output;

	var instructionPointer = 0;
	var opCode = memory[instructionPointer];
	var instruction = opCode % 100;
	int? programOutput = null;
	
	while (instruction != 99)
	{
		Operation operation;
		
		if (operations.TryGetValue(instruction, out operation)) {
			operation(memory, ref instructionPointer, opCode, programInput, ref programOutput);			
		}
		else
		{
			throw new InvalidOperationException($"Unrecognized opcode {instruction}");
		}

		opCode = memory[instructionPointer];
		instruction = opCode % 100;
	}

	return programOutput;
}

int Read(int[] memory, int pointer, int parameter, int mode)
{
	var position = pointer + parameter;
	if (mode == 1) {
		return memory[position];
	} else {
		return memory[memory[position]];
	}
}

void Write(int[] memory, int pointer, int parameter, int value)
{
	var position = pointer + parameter;
	memory[memory[position]] = value;
}

int GetMode(int instruction, int position)
{
	return (((instruction / 100) / (int)Math.Pow(10, position - 1)) % 10);
}

public void Add(int[] memory, ref int pointer, int opCode, int input, ref int? output)
{
	var parameter1 = Read(memory, pointer, 1, GetMode(opCode, 1));
	var parameter2 = Read(memory, pointer, 2, GetMode(opCode, 2));

	Write(memory, pointer, 3, parameter1 + parameter2);

	pointer += 4;
}

public void Multiply(int[] memory, ref int pointer, int opCode, int input, ref int? output)
{
	var parameter1 = Read(memory, pointer, 1, GetMode(opCode, 1));
	var parameter2 = Read(memory, pointer, 2, GetMode(opCode, 2));

	Write(memory, pointer, 3, parameter1 * parameter2);

	pointer += 4;
}

public void Input(int[] memory, ref int pointer, int opCode, int input, ref int? output)
{
	Write(memory, pointer, 1, input);
	pointer += 2;
}

public void Output(int[] memory, ref int pointer, int opCode, int input, ref int? output)
{
	output = Read(memory, pointer, 1, 0);
	pointer += 2;
}

string ReadOpCodes()
{
	var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.5.txt");
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