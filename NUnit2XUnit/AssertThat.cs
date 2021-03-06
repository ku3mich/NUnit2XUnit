﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SyntaxConverters;

namespace NUnit2XUnit
{
    using static SyntaxFactory;

    public class AssertThat : MethodInvocationFactoryBase
    {
        public static IConverterFactory Factory { get; } = new AssertThat();

        protected override ISyntaxConverter CreateConverter(MethodInvocation param)
        {
            if (!param.IsMethodCallTo("Assert", "That"))
            {
                return null;
            }

            var args = param.Arguments;
            if (args.Count != 2)
            {
                return null;
            }

            if (!(args[1].Expression is InvocationExpressionSyntax equalTo))
            {
                return null;
            }

            var isEqualTo = new MethodInvocation(equalTo);
            if (!isEqualTo.IsMethodCallTo("Is", "EqualTo"))
            {
                return null;
            }

            if (isEqualTo.Arguments.Count != 1)
            {
                return null;
            }

            var expected = isEqualTo.Arguments[0];
            var actual = args[0];

            return new TriviaWhitespace.Link(new Converter(param, new AssertArguments(expected, actual)), param.Node);
        }

        public class Converter : ConverterBase<(MethodInvocation Invocation, AssertArguments AssertArgs)>
        {
            public Converter(MethodInvocation invocation, AssertArguments assertArgs) : base((invocation, assertArgs))
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
}
