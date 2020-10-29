using Microsoft.CodeAnalysis.CSharp.Syntax;
using SyntaxConverters;

namespace NUnit2XUnit
{
    public class TestFixtureStripper : TypedConverterFactory<AttributeSyntax>
    {
        public static IConverterFactory Factory { get; } = new TestFixtureStripper();

        protected override ISyntaxConverter CreateConverter(AttributeSyntax node) =>
            node.Name?.ToString() == "TestFixture" && node.Parent?.Parent is ClassDeclarationSyntax
                ? ConstantConverter.Null
                : null;
    }
}
