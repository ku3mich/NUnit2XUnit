using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SyntaxConverters;

namespace NUnit2XUnit
{
    public class UsingConverter : IConverterFactory
    {
        public static IConverterFactory Factory { get; } = new UsingConverter();

        public ISyntaxConverter CreateConverter(SyntaxNode node)
        {
            var usingNode = node as UsingDirectiveSyntax;

            var result = usingNode?.Name.ToString() == "NUnit.Framework"
                ? new TriviaWhitespace.Link(XUnitUsingConverter.Instance, node)
                : null;

            return result;
        }
    }
}
