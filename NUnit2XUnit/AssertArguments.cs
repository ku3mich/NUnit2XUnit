using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NUnit2XUnit
{
    using static SyntaxFactory;

    public readonly struct AssertArguments
    {
        public readonly SyntaxNodeOrToken Expected;
        public readonly SyntaxNodeOrToken Actual;
        public ArgumentListSyntax Arguments => ArgumentList(SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[] { Expected, Token(SyntaxKind.CommaToken), Actual }));

        public AssertArguments(SyntaxNodeOrToken expected, SyntaxNodeOrToken actual)
        {
            Expected = expected;
            Actual = actual;
        }
    }
}
