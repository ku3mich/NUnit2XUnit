// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Copyright (C) 2019 Dmitry Yakimenko (detunized@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NUnit2XUnit
{
    using static SyntaxFactory;

    public class MethodCallNameRewriter : IntermediateConverter<MethodCallState>
    {
        private readonly string Identifier;
        private readonly string Method;

        public MethodCallNameRewriter(MethodCallState state, string identifier, string method)
            : base(state)
        {
            Identifier = identifier;
            Method = method;
        }

        public override ExpressionSyntax Convert()
        {
            InvocationExpressionSyntax expression = InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(Identifier),
                        IdentifierName(Method)))
                .WithArgumentList(ComposeArguments())
                .NormalizeWhitespace()
                .WithTriviaFrom(State.Node);

            return expression;
        }

        protected virtual ArgumentListSyntax ComposeArguments()
        {
            return ArgumentList(State.Arguments);
        }
    }
}