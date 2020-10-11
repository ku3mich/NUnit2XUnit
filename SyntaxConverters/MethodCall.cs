// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxConverters
{
    public readonly struct MethodCall
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

        public MethodCall(InvocationExpressionSyntax node, MemberAccessExpressionSyntax accessedMember)
        {
            Node = node;
            AccessedMember = accessedMember;
        }
    }
}
