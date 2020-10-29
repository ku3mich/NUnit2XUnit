using Microsoft.CodeAnalysis.CSharp.Syntax;
using SyntaxConverters;

namespace NUnit2XUnit
{
    public class Test2FactConverter : TypedConverterFactory<AttributeSyntax>
    {
        public static IConverterFactory Factory { get; } = new Test2FactConverter();

        protected override ISyntaxConverter CreateConverter(AttributeSyntax node)
        {
            if (node.Name?.ToString() != "Test")
            {
                return null;
            }

            if (!(node.Parent?.Parent is MethodDeclarationSyntax))
            {
                return null;
            }

            var result = new TriviaWhitespace.Link(
                new ConstantConverter(AttributeSyntaxFactory.Fact()),
                node);

            return result;
        }
    }
}
