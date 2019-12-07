<Query Kind="Program" />

void Main()
{
	Amplify(new[] { 4, 3, 2, 1, 0 }, "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0").Dump();
	Amplify(new[] { 0, 1, 2, 3, 4 }, "3,23,3,24,1002,24,10,24,1002,23,-1,23,101, 5, 23, 23, 1, 24, 23, 23, 4, 23, 99, 0, 0").Dump();
	Amplify(new[] { 1, 0, 4, 3, 2 }, "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0").Dump();

	var max = 0;
	var rawCodes = ReadOpCodes();

	var phaseSettings = Permutate(new[] { 0, 1, 2, 3, 4 });

	foreach (var phaseSetting in phaseSettings)
	{
		var output = Amplify(phaseSetting.ToArray(), rawCodes);
		max = Math.Max(max, output);
	}

	max.Dump();
}

int Amplify(int[] phaseSettings, string rawCodes)
{
	int? input = 0;

	foreach (var phaseSetting in phaseSettings)
	{
		var opcodes = Parse(rawCodes);
		input = new Amplifier(phaseSetting, opcodes).Run(input.Value);
	}

	return input.Value;
}

delegate void Operation(int opCode);

class Amplifier
{
	private int[] _memory;
	private int _pointer;
	private Queue<int> _inputs;
	private int _output;
	private Dictionary<int, Operation> _operations;
	
	public bool Halted = false;

	public Amplifier(int phaseSetting, int[] memory)
	{
		_memory = memory;
		_inputs = new Queue<int>(new[] { phaseSetting });
		_operations = new Dictionary<int, Operation>();

		_operations[1] = Add;
		_operations[2] = Multiply;
		_operations[3] = Input;
		_operations[4] = Output;
		_operations[5] = JumpIfTrue;
		_operations[6] = JumpIfFalse;
		_operations[7] = LessThan;
		_operations[8] = Equals;
	}

	public int Run(int input)
	{
		_inputs.Enqueue(input);

		var opCode = _memory[_pointer];
		var instruction = opCode % 100;

		while (instruction != 99)
		{
			Operation operation;

			if (_operations.TryGetValue(instruction, out operation))
			{
				operation(opCode);

				if (instruction == 4)
				{
					return _output;
				}
			}
			else
			{
				throw new InvalidOperationException($"Unrecognized opcode {instruction}");
			}

			opCode = _memory[_pointer];
			instruction = opCode % 100;
		}
		
		Halted = true;
		
		return _output;
	}
	
	int Read(int parameter, int mode)
	{
		var position = _pointer + parameter;
		if (mode == 1)
		{
			return _memory[position];
		}
		else
		{
			return _memory[_memory[position]];
		}
	}

	void Write(int parameter, int value)
	{
		var position = _pointer + parameter;
		_memory[_memory[position]] = value;
	}
	
	public void Add(int opCode)
	{
		var parameter1 = Read(1, GetMode(opCode, 1));
		var parameter2 = Read(2, GetMode(opCode, 2));

		Write(3, parameter1 + parameter2);

		_pointer += 4;
	}

	public void Multiply(int opCode)
	{
		var parameter1 = Read(1, GetMode(opCode, 1));
		var parameter2 = Read(2, GetMode(opCode, 2));

		Write(3, parameter1 * parameter2);

		_pointer += 4;
	}

	public void Input(int opCode)
	{
		Write(1, _inputs.Dequeue());
		_pointer += 2;
	}

	public void Output(int opCode)
	{
		_output = Read(1, 0);
		_pointer += 2;
	}

	public void JumpIfTrue(int opCode)
	{
		var parameter1 = Read(1, GetMode(opCode, 1));
		var parameter2 = Read(2, GetMode(opCode, 2));

		if (parameter1 != 0)
		{
			_pointer = parameter2;
		}
		else
		{
			_pointer += 3;
		}
	}

	public void JumpIfFalse(int opCode)
	{
		var parameter1 = Read(1, GetMode(opCode, 1));
		var parameter2 = Read(2, GetMode(opCode, 2));

		if (parameter1 == 0)
		{
			_pointer = parameter2;
		}
		else
		{
			_pointer += 3;
		}
	}

	public void LessThan(int opCode)
	{
		var parameter1 = Read(1, GetMode(opCode, 1));
		var parameter2 = Read(2, GetMode(opCode, 2));

		Write(3, (parameter1 < parameter2) ? 1 : 0);

		_pointer += 4;
	}

	public void Equals(int opCode)
	{
		var parameter1 = Read(1, GetMode(opCode, 1));
		var parameter2 = Read(2, GetMode(opCode, 2));

		Write(3, (parameter1 == parameter2) ? 1 : 0);

		_pointer += 4;
	}
	
	int GetDigit(int source, int position)
	{
		return ((source / (int)Math.Pow(10, position - 1)) % 10);
	}

	int GetMode(int instruction, int position)
	{
		return GetDigit(instruction / 100, position);
	}
}

private static IEnumerable<int[]> Permutate(int[] source)
{
    if (source.Length == 1) return new List<int[]> { source };

	var permutations = from c in source
					   from p in Permutate(source.Where(x => x != c).ToArray())
					   select p.Prepend(c).ToArray();

	return permutations;
}

string ReadOpCodes()
{
	var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.7.txt");
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