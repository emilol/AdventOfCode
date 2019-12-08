using Day2.App;
using Shouldly;
using Xunit;

namespace Day2.Tests
{
    public class Part2Tests
    {
        [Theory]
        [InlineData("1,0,0,0,99", new[] { 2, 0, 0, 0, 99 })]
        [InlineData("2,3,0,3,99", new[] { 2, 3, 0, 6, 99 })]
        [InlineData("2,4,4,5,99,0", new[] { 2, 4, 4, 5, 99, 9801 })]
        [InlineData("1,1,1,4,99,5,6,0,99", new[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 })]
        public void Test(string rawCodes, int[] expected)
        {
            var output = Part1.Run(Program.Parse(rawCodes));
            output.ShouldBe(expected);
        }
    }
}
