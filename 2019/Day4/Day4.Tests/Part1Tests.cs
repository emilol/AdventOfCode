using Day4.App;
using Shouldly;
using Xunit;

namespace Day4.Tests
{
    public class Part1Tests
    {
        [Theory]
        [InlineData(111111, true)]
        [InlineData(223450, false)]
        [InlineData(123789, false)]
        public void Test(int input, bool expected)
        {
            var output = Part1.IsMatch(input);
            output.ShouldBe(expected);
        }
    }
}
