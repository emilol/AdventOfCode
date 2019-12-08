using System;
using System.IO;
using System.Linq;

namespace Day2.App
{
    public static class Part2
	{

        public static int Run(int[] memory, int? noun = null, int? verb = null)
        {
            if (noun.HasValue && verb.HasValue)
            {
                memory = InitializeMemory(noun.Value, verb.Value, memory);
            }

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

        public static int[] InitializeMemory(int noun, int verb, int[] opCodes)
        {
            opCodes[1] = noun;
            opCodes[2] = verb;

            return opCodes;
        }
	}
}