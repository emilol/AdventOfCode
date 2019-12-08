using Day3.App;
using Shouldly;
using Xunit;

namespace Day3.Tests
{
    public class Part1Tests
    {
        [Theory]
        [InlineData("test1.txt", 159)]
        [InlineData("test2.txt", 135)]
        public void Test1(string inputFile, int expected)
        {
            var input = Program.Read(inputFile);
            var output = Part1.Run(input);

            output.ShouldBe(expected);
        }
    }   
}
