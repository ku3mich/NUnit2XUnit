using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NUnit2XUnit
{
    using static SyntaxFactory;

    public static class AttributeSyntaxFactory
    {
        public static AttributeSyntax Create(string identifier, AttributeArgumentListSyntax args = null) => Attribute(IdentifierName(identifier), args);
        public static AttributeSyntax Fact() => Create("Fact");
        public static AttributeSyntax InlineData(AttributeArgumentListSyntax args) => Create("InlineData", args);
        public static AttributeSyntax Theory() => Create("Theory");
        public static AttributeListSyntax List(AttributeSyntax item)
            => SyntaxFactory.AttributeList(SeparatedList(new[] { item }));

        public static AttributeSyntax Category(ExpressionSyntax category)
            => Create("Trait",
                AttributeArgumentList(
                        SeparatedList<AttributeArgumentSyntax>(
                            new SyntaxNodeOrToken[] {
                                AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal("category"))),
                                Token(SyntaxKind.CommaToken),
                                AttributeArgument(category)
                            })));
    }
}
