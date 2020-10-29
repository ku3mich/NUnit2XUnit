using Microsoft.CodeAnalysis.CSharp.Syntax;
using SyntaxConverters;

namespace NUnit2XUnit
{
    //todo: generalize TestCase2InlineData, Test2FactConverter
    public class TestCase2InlineData : TypedConverterFactory<AttributeSyntax>
    {
        public static IConverterFactory Factory { get; } = new TestCase2InlineData();

        protected override ISyntaxConverter CreateConverter(AttributeSyntax node)
        {
            if (node.Name?.ToString() != "TestCase")
            {
                return null;
            }

            if (!(node.Parent?.Parent is MethodDeclarationSyntax))
            {
                return null;
            }

            var result = new TriviaWhitespace.Link(
                new ConstantConverter(AttributeSyntaxFactory.InlineData(node.ArgumentList)),
                node);

            return result;
        }
    }
}
