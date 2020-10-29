// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxConverters
{
    public readonly struct MethodInvocation
    {
        public readonly InvocationExpressionSyntax Node;
        public readonly MemberAccessExpressionSyntax AccessedMember => Node.Expression as MemberAccessExpressionSyntax;
        public SeparatedSyntaxList<ArgumentSyntax> Arguments => Node.ArgumentList.Arguments;

        public string AccessedIdentifier => AccessedMember.Expression.ToString();
        public string AccessedMethod => AccessedMember.Name.ToString();

        public bool IsMethodCallTo([NotNull] string target)
            => AccessedIdentifier == target;

        public bool IsMethodCallTo([NotNull] string target, [NotNull] string method)
            => IsMethodCallTo(target) && AccessedMethod == method;

        public MethodInvocation(InvocationExpressionSyntax node) => Node = node;
    }
}
