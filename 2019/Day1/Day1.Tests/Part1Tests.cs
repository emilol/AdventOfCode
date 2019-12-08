using Day1.App;
using Shouldly;
using Xunit;

namespace Day1.Tests
{
    public class Part1Tests
    {
        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void Test(int mass, int expected)
        {
            var fuel = Part1.Fuel(mass);
            fuel.ShouldBe(expected);
        }
    }
}
