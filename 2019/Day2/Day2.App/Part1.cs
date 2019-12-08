using System;

namespace Day2.App
{
    public static class Part1
	{
        public static int[] Run(int[] opcodes, int? a = null, int? b = null)
        {
            if (a.HasValue && b.HasValue)
            {
                opcodes = InitializeOpCodes(a.Value, b.Value, opcodes);
            }

            var index = 0;
            var op = Instruction.Continue;

            while (op != Instruction.Halt)
            {
                op = opcodes[index];

                var args = new[] { opcodes[opcodes[index + 1]], opcodes[opcodes[index + 2]] };
                var output = opcodes[index + 3];

                if (op == Instruction.Add)
                {
                    opcodes[output] = args[0] + args[1];
                }
                else if (op == Instruction.Multiply)
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

        public static int[] InitializeOpCodes(int a, int b, int[] opCodes)
        {
            opCodes[1] = a;
            opCodes[2] = b;

            return opCodes;
        }
    }
}