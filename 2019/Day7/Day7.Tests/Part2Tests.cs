using System.Linq;
using Day7.App;
using Shouldly;
using Xunit;

namespace Day7.Tests
{
    public class Part2Tests
    {
        [Theory]
        [InlineData("test4.txt", new[] { 9, 8, 7, 6, 5 }, 139629729)]
        [InlineData("test5.txt", new[] { 9, 7, 8, 5, 6 }, 18216)]
        public void Test(string inputFile, int[] phaseSettings, int expected)
        {
            var input = Program.Read(inputFile).ToArray();
            var output = App.Part2.Methods.Amplify(input, phaseSettings);

            output.ShouldBe(expected);
        }
    }
}