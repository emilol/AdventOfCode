using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7.App.Part2
{
    public static class Methods
	{
        public static int Part2(this int[] opcodes, IEnumerable<int[]> phaseSettings)
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
            var input = 0;
            var amplifiers = new Queue<Amplifier>(phaseSettings.Select(s => new Amplifier(s, opcodes)));

            while (amplifiers.Any(a => !a.Halted))
            {
                var amplifier = amplifiers.Dequeue();

                input = amplifier.Run(input);
                amplifiers.Enqueue(amplifier);
            }

            return input;
        }
	}
}