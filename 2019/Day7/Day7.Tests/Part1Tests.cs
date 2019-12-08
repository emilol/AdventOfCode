using Day7.App;
using Day7.App.Part1;
using Shouldly;
using Xunit;

namespace Day7.Tests
{
    public class Part1Tests
    {
        [Theory]
        [InlineData("test1.txt", new[] { 4, 3, 2, 1, 0 }, 43210)]
        [InlineData("test2.txt", new[] { 0, 1, 2, 3, 4 }, 54321)]
        [InlineData("test3.txt", new[] { 1, 0, 4, 3, 2 }, 65210)]
        public void Test1(string inputFile, int[] phaseSettings, int expected)
        {
            var input = Program.Read(inputFile);
            var output = input.Amplify(phaseSettings);

            output.ShouldBe(expected);
        }
    }
}
