<Query Kind="Program" />

void Main()
{
	Amplify(new[] { 9, 8, 7, 6, 5 }, "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5").Dump();
	Amplify(new[] { 9, 7, 8, 5, 6 }, "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10").Dump();

	var max = 0;
	var rawCodes = ReadOpCodes();

	var phaseSettings = Permutate(new[] { 5,6,7,8,9 });

	foreach (var phaseSetting in phaseSettings)
	{
		var output = Amplify(phaseSetting.ToArray(), rawCodes);
		max = Math.Max(max, output);
	}

	max.Dump();
}

int Amplify(int[] phaseSettings, string rawCodes)
{
	int input = 0;
	var amplifiers = new Queue<Amplifier>(phaseSettings.Select(s => new Amplifier(s, Parse(rawCodes))));

	while (amplifiers.Any(a => !a.Halted))
	{
		var amplifier = amplifiers.Dequeue();

		input = amplifier.Run(input);
		amplifiers.Enqueue(amplifier);
	}

	return input;
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