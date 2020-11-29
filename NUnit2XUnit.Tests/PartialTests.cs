using Text.Diff;
using Xunit;
using Xunit.Abstractions;

namespace NUnit2XUnit.Tests
{
    public class PartialTests : TestBase
    {
        public PartialTests(ITestOutputHelper console, ITextDiff diff) : base(console, diff)
        {
        }

        [Theory]
        [ConvertCase("sample/usingInsideNs.cs")]
        public void Using(ConvertCase c)
        {
            var tree = Parse(c.Source);
            var visitor = CreateVisitor();
            var rewrited = visitor.VisitRoot(tree);

            CompareResults(c.Source, c.Expected, rewrited);
        }
    }
}
