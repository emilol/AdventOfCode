using Day1.App;
using Shouldly;
using Xunit;

namespace Day1.Tests
{
    public class Part2Tests
    {
        [Theory]
        [InlineData(14, 2, 2)]
        [InlineData(1969, 654, 966)]
        [InlineData(100756, 33583, 50346)]
        public void Test(int mass, int expectedFuel, int expectedTotalFuel)
        {
            var fuel = Part2.Fuel(mass);
            fuel.ShouldBe(expectedFuel);

            var total = Part2.TotalFuel(fuel);
            total.ShouldBe(expectedTotalFuel);
        }
    }
}