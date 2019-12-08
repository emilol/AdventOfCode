
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4.App
{
    public static class Part1
	{
        public static int Run()
        {
            return Enumerable.Range(402328, 864247 - 402328)
                .Where(IsMatch)
                .Count();
        }

        public static bool IsMatch(int password)
        {
            var digits = ToDigits(password);
            return IsOrdered(digits) && HasDouble(digits);
        }

        public static bool IsOrdered(IEnumerable<int> digits)
        {
            var previous = -1;
            foreach (var digit in digits)
            {
                if (previous > digit)
                {
                    return false;
                }
                previous = digit;
            }

            return true;
        }

        public static bool HasDouble(IEnumerable<int> digits)
        {
            return digits.GroupBy(d => d).Any(d => d.Count() >= 2);
        }

        public static IEnumerable<int> ToDigits(int password)
        {
            return password.ToString().Select(d => (int)char.GetNumericValue(d));
        }
	}
}