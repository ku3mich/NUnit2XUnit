// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SyntaxConverters;

namespace SyntaxConverters
{
    public abstract class MethodCallComposerBase : IConverterComposer
    {
        public Task<IExpressionSyntaxConverter> ComposeConverter(ExpressionSyntax node)
        {
            if (!(node is InvocationExpressionSyntax invocationExpressionSyntax))
                return null;

            MemberAccessExpressionSyntax accessedMember = invocationExpressionSyntax.Expression as MemberAccessExpressionSyntax;

            return accessedMember == null ?
                ConverterFactory(new MethodCall(invocationExpressionSyntax, accessedMember))
                : null;
        }

        protected abstract Task<IExpressionSyntaxConverter> ConverterFactory(MethodCall state);
    }
}
