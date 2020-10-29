using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NUnit2XUnit
{
    using static SyntaxFactory;

    public static class AttributeSyntaxFactory
    {
        public static SyntaxNode Create(string identifier) => Attribute(IdentifierName(identifier));
        public static SyntaxNode Fact() => Create("Fact");
        public static SyntaxNode Theory() => Create("Theory");
        public static SyntaxNode Category(ExpressionSyntax category)
            => Attribute(
                IdentifierName("Trait"),
                AttributeArgumentList(
                        SeparatedList<AttributeArgumentSyntax>(
                            new SyntaxNodeOrToken[] {
                                AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal("category"))),
                                Token(SyntaxKind.CommaToken),
                                AttributeArgument(category)
                            })));
    }
}
