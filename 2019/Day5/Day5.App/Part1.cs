using System;
using System.Collections.Generic;

namespace Day5.App
{
    public static class Part1
	{

		delegate void Operation(int[] memory, ref int pointer, int opCode, int input, ref int? output);

		public static int? Run(int[] memory, int programInput)
		{
            var operations = new Dictionary<int, Operation>
            {
                [1] = Add, 
                [2] = Multiply, 
                [3] = Input, 
                [4] = Output
            };


            var instructionPointer = 0;
			var opCode = memory[instructionPointer];
			var instruction = opCode % 100;
			int? programOutput = null;

			while (instruction != 99)
			{
                if (operations.TryGetValue(instruction, out var operation))
				{
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

        public static int Read(int[] memory, int pointer, int parameter, int mode)
		{
			var position = pointer + parameter;
			if (mode == 1)
			{
				return memory[position];
			}

            return memory[memory[position]];
        }

        public static void Write(int[] memory, int pointer, int parameter, int value)
		{
			var position = pointer + parameter;
			memory[memory[position]] = value;
		}

        public static int GetMode(int instruction, int position)
		{
			return instruction / 100 / (int)Math.Pow(10, position - 1) % 10;
		}

		public static void Add(int[] memory, ref int pointer, int opCode, int input, ref int? output)
		{
			var parameter1 = Read(memory, pointer, 1, GetMode(opCode, 1));
			var parameter2 = Read(memory, pointer, 2, GetMode(opCode, 2));

			Write(memory, pointer, 3, parameter1 + parameter2);

			pointer += 4;
		}

		public static void Multiply(int[] memory, ref int pointer, int opCode, int input, ref int? output)
		{
			var parameter1 = Read(memory, pointer, 1, GetMode(opCode, 1));
			var parameter2 = Read(memory, pointer, 2, GetMode(opCode, 2));

			Write(memory, pointer, 3, parameter1 * parameter2);

			pointer += 4;
		}

		public static void Input(int[] memory, ref int pointer, int opCode, int input, ref int? output)
		{
			Write(memory, pointer, 1, input);
			pointer += 2;
		}

		public static void Output(int[] memory, ref int pointer, int opCode, int input, ref int? output)
		{
			output = Read(memory, pointer, 1, 0);
			pointer += 2;
		}
	}
}