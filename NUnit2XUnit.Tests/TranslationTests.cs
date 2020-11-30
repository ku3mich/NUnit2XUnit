using Text.Diff;
using Xunit;
using Xunit.Abstractions;

namespace NUnit2XUnit.Tests
{
    public class TranslationTests : TestBase
    {
        public TranslationTests(ITestOutputHelper console, ITextDiff diff) : base(console, diff)
        {
        }

        [Theory]
        [CasesFolder("sample/auto/*.cs")]
        public void Translates(ConvertCase c)
        {
            var tree = Parse(c.Source);
            var visitor = CreateVisitor();
            var rewrited = visitor.VisitRoot(tree);

            CompareResults(c.Source, c.Expected, rewrited);
        }
    }
}

