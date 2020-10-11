// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SyntaxConverters;

namespace SyntaxConverters
{
    using static SyntaxFactory;

    public class MethodCallNameRewriter : ConverterBase<MethodCall>
    {
        private readonly string Identifier;
        private readonly string Method;

        public MethodCallNameRewriter(MethodCall state, string identifier, string method)
            : base(state)
        {
            Identifier = identifier;
            Method = method;
        }

        public override Task<ExpressionSyntax> Convert()
        {
            InvocationExpressionSyntax expression = InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(Identifier),
                        IdentifierName(Method)))
                .WithArgumentList(ComposeArguments())
                .NormalizeWhitespace()
                .WithTriviaFrom(State.Node);

            return Task.FromResult((ExpressionSyntax)expression);
        }

        protected virtual ArgumentListSyntax ComposeArguments()
        {
            return ArgumentList(State.Arguments);
        }
    }
}
