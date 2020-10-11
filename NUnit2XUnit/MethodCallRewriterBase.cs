// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using Microsoft.CodeAnalysis.CSharp.Syntax;
using SyntaxConverters;

namespace NUnit2XUnit
{
    public abstract class MethodCallComposerBase : IConverterComposer
    {
        public IExpressionSyntaxConverter ComposeConverter(ExpressionSyntax node)
        {
            InvocationExpressionSyntax invocationExpressionSyntax = node as InvocationExpressionSyntax;
            if (invocationExpressionSyntax == null)
                return null;

            MemberAccessExpressionSyntax accessedMember = invocationExpressionSyntax.Expression as MemberAccessExpressionSyntax;

            return accessedMember == null ?
                ConverterFactory(new MethodCall(invocationExpressionSyntax, accessedMember))
                : null;
        }

        protected abstract IExpressionSyntaxConverter ConverterFactory(MethodCall state);
    }
}
