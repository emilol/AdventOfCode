using Day3.App;
using Shouldly;
using Xunit;

namespace Day3.Tests
{
    public class Part2Tests
    {
        [Theory]
        [InlineData("test1.txt", 610)]
        [InlineData("test2.txt", 410)]
        public void Test1(string inputFile, int expected)
        {
            var input = Program.Read(inputFile);
            var output = Part2.Run(input);

            output.ShouldBe(expected);
        }
    }
}