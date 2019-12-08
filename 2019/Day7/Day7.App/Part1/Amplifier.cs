using System;
using System.Collections.Generic;

namespace Day7.App.Part1
{
    public class Amplifier
    {
        private readonly int[] _memory;
        private readonly Queue<int> _inputs;
        private readonly Dictionary<int, Operation> _operations;

        private int _pointer;
        private int _output;

        public bool Halted;

        public Amplifier(int phaseSetting, int[] memory)
        {
            _memory = memory;
            _inputs = new Queue<int>(new[] { phaseSetting });
            _operations = new Dictionary<int, Operation>
            {
                [1] = Add,
                [2] = Multiply,
                [3] = Input,
                [4] = Output,
                [5] = JumpIfTrue,
                [6] = JumpIfFalse,
                [7] = LessThan,
                [8] = Equals
            };
        }

        public int Run(int input)
        {
            _inputs.Enqueue(input);

            var opCode = _memory[_pointer];
            var instruction = opCode % 100;

            while (instruction != 99)
            {
                if (_operations.TryGetValue(instruction, out var operation))
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
}