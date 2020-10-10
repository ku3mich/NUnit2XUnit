// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Copyright (C) 2019 Dmitry Yakimenko (detunized@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NUnit2XUnit
{
    public readonly struct MethodCallState
    {
        public readonly InvocationExpressionSyntax Node;
        public readonly MemberAccessExpressionSyntax AccessedMember;
        public SeparatedSyntaxList<ArgumentSyntax> Arguments => Node.ArgumentList.Arguments;

        public string AccessedIdentifier => (AccessedMember.Expression as IdentifierNameSyntax)?.Identifier.ValueText;

        public bool IsMethodCallTo([NotNull] string target)
        {
            return AccessedIdentifier == target;
        }

        public bool IsMethodCallTo([NotNull] string target, [NotNull] string method)
        {
            return IsMethodCallTo(target) && AccessedMember.Name.Identifier.ValueText == method;
        }

        public MethodCallState(InvocationExpressionSyntax node, MemberAccessExpressionSyntax accessedMember)
        {
            Node = node;
            AccessedMember = accessedMember;
        }
    }
}