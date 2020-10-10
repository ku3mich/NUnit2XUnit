using Xunit;
using Xunit.Abstractions;
using XUnit;

namespace NUnit2XUnit.Tests
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper Console;

        public UnitTest1(ITestOutputHelper console)
        {
            Console = console;
        }

        [Theory]
        [FileContent("sample/test._cs")]
        public void Test1(string s)
        {
            Console.WriteLine(s);
        }
    }
}
