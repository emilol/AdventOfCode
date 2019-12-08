using Day4.App;
using Shouldly;
using Xunit;

namespace Day4.Tests
{
    public class Part2Tests
    {
        [Theory]
        [InlineData(111111, false)]
        [InlineData(223450, false)]
        [InlineData(123789, false)]
        [InlineData(112233, true)]
        [InlineData(123444, false)]
        [InlineData(111122, true)]
        public void Test(int input, bool expected)
        {
            var output = Part2.IsMatch(input);
            output.ShouldBe(expected);
        }
    }
}