using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SyntaxConverters;

namespace NUnit2XUnit
{
    using static SyntaxFactory;

    public class AssertThatConverter : ConverterBase<(MethodInvocation Invocation, AssertArguments AssertArgs)>
    {
        public AssertThatConverter(MethodInvocation invocation, AssertArguments assertArgs) : base((invocation, assertArgs))
        {
        }

        public override SyntaxNode Convert() => InvocationExpression(
                   MemberAccessExpression(
                       SyntaxKind.SimpleMemberAccessExpression,
                       IdentifierName("Assert"),
                       IdentifierName("Equal")))
               .WithArgumentList(
                   ArgumentList(
                       SeparatedList<ArgumentSyntax>(
                           new SyntaxNodeOrToken[] { State.AssertArgs.Expected, Token(SyntaxKind.CommaToken), State.AssertArgs.Actual })));
    }
}
