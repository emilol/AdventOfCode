using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7.App.Part1
{
    public static class Methods
    {
        public static int Part1(this int[] opcodes, IEnumerable<int[]> phaseSettings)
        {
            var max = 0;

            foreach (var phaseSetting in phaseSettings)
            {
                var output = Amplify(opcodes, phaseSetting.ToArray());
                max = Math.Max(max, output);
            }

            return max;
        }

        public static int Amplify(this int[] opcodes, IEnumerable<int> phaseSettings)
        {
            int? input = 0;

            foreach (var phaseSetting in phaseSettings)
            {
                input = new Amplifier(phaseSetting, opcodes).Run(input.Value);
            }

            return input.Value;
        }
    }
}