using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3.App
{
    public static class Part1
	{
		public static int Run(IEnumerable<string[]> wireInstructions)
		{
			var positions = wireInstructions
				.Select(w => Positions(w)
					.Where(p => p.dist > 0)
					.OrderBy(p => p.dist));

			var intersections = positions.First().Intersect(positions.Last(), new PositionComparer());

			return intersections.First().dist;
		}

        public static IEnumerable<(int x, int y, int dist)> Positions(string[] instructions)
		{
			var origin = (x: 0, y: 0, dist: 0);
			var previousPosition = origin;

			yield return origin;

			foreach (var instruction in instructions)
			{
				var direction = instruction[0].ToString();
				var distance = int.Parse(instruction.Substring(1));

				for (int i = 1; i <= distance; i++)
				{
					var position = previousPosition;

					if (direction == "R")
					{
						position.x = previousPosition.x + 1;
						position.dist = Math.Abs(position.x) + Math.Abs(position.y);

						yield return position;

						previousPosition = position;
						continue;
					}

					if (direction == "L")
					{
						position.x = previousPosition.x - 1;
						position.dist = Math.Abs(position.x) + Math.Abs(position.y);

						yield return position;

						previousPosition = position;
						continue;
					}

					if (direction == "U")
					{
						position.y = previousPosition.y + 1;
						position.dist = Math.Abs(position.x) + Math.Abs(position.y);

						yield return position;

						previousPosition = position;
						continue;
					}

					if (direction == "D")
					{
						position.y = previousPosition.y - 1;
						position.dist = Math.Abs(position.x) + Math.Abs(position.y);

						yield return position;

						previousPosition = position;
						continue;
					}
				}
			}
		}

		public class PositionComparer : IEqualityComparer<(int x, int y, int dist)>
		{
			public bool Equals((int x, int y, int dist) a, (int x, int y, int dist) b)
			{
				return a.x == b.x && a.y == b.y;
			}

			public int GetHashCode((int x, int y, int dist) a)
			{
				int hCode = a.x ^ a.y;
				return hCode.GetHashCode();
			}
		}
	}
}