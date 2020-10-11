// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NUnit2XUnit
{
    using static SyntaxFactory;

    public class NunitToXunitRewriter : CSharpSyntaxRewriter
    {
        public static NunitToXunitRewriter Instance { get; } = new NunitToXunitRewriter();

        protected NunitToXunitRewriter()
        {
        }

        public override SyntaxNode VisitUsingDirective(UsingDirectiveSyntax node)
        {
            SyntaxNode newNode = TryConvertUsingNunit(node);
            if (newNode != null)
                return newNode;

            return base.VisitUsingDirective(node);
        }

        public override SyntaxNode VisitAttributeList(AttributeListSyntax node)
        {
            if (ShouldRemoveTestFixture(node))
                return null;

            SyntaxNode newNode = TryConvertTestAttribute(node);
            if (newNode != null)
                return newNode;

            return base.VisitAttributeList(node);
        }

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            SyntaxNode newNode = TryConvertAssertThatIsEqualTo(node);
            if (newNode != null)
                return newNode;

            return base.VisitInvocationExpression(node);
        }

        private SyntaxNode TryConvertUsingNunit(UsingDirectiveSyntax node)
        {
            if (node.Name.ToString() != "NUnit.Framework")
                return null;

            return
                UsingDirective(IdentifierName("Xunit"))
                .NormalizeWhitespace()
                .WithTriviaFrom(node);
        }

        private bool ShouldRemoveTestFixture(AttributeListSyntax node)
        {
            return node.Attributes.Count == 1
                && node.Attributes[0].Name.ToString() == "TestFixture"
                && node.Parent is ClassDeclarationSyntax;
        }

        private SyntaxNode TryConvertTestAttribute(AttributeListSyntax node)
        {
            if (node.Attributes[0].Name.ToString() != "Test")
                return null;

            if (!(node.Parent is MethodDeclarationSyntax))
                return null;

            AttributeListSyntax result = AttributeList(SingletonSeparatedList(CreateFact()))
                .NormalizeWhitespace()
                .WithTriviaFrom(node);

            return result;

        }

        private SyntaxNode CreateFact()
        {
            return Attribute(IdentifierName("Fact"));
        }

        // Converts Assert.That(actual, Is.EqualTo(expected)) to Assert.Equal(expected, actual)
        private SyntaxNode TryConvertAssertThatIsEqualTo(InvocationExpressionSyntax node)
        {
            if (!IsMethodCall(node, "Assert", "That"))
                return null;

            ArgumentSyntax[] assertThatArgs = GetCallArguments(node);
            if (assertThatArgs.Length != 2)
                return null;

            ExpressionSyntax isEqualTo = assertThatArgs[1].Expression;
            if (!IsMethodCall(isEqualTo, "Is", "EqualTo"))
                return null;

            ArgumentSyntax[] isEqualToArgs = GetCallArguments(isEqualTo);
            if (isEqualToArgs.Length != 1)
                return null;

            ArgumentSyntax expected = isEqualToArgs[0];
            ArgumentSyntax actual = assertThatArgs[0];

            return
                InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("Assert"),
                        IdentifierName("Equal")))
                .WithArgumentList(
                    ArgumentList(
                        SeparatedList<ArgumentSyntax>(
                            new SyntaxNodeOrToken[] { expected, Token(SyntaxKind.CommaToken), actual })))
                .NormalizeWhitespace()
                .WithTriviaFrom(node);
        }

        private bool IsMethodCall(ExpressionSyntax node, string objekt, string method)
        {
            InvocationExpressionSyntax invocation = node as InvocationExpressionSyntax;
            if (invocation == null)
                return false;

            MemberAccessExpressionSyntax memberAccess = invocation.Expression as MemberAccessExpressionSyntax;
            if (memberAccess == null)
                return false;

            if ((memberAccess.Expression as IdentifierNameSyntax)?.Identifier.ValueText != objekt)
                return false;

            if (memberAccess.Name.Identifier.ValueText != method)
                return false;

            return true;
        }

        private ArgumentSyntax[] GetCallArguments(ExpressionSyntax node)
        {
            return ((InvocationExpressionSyntax)node).ArgumentList.Arguments.ToArray();
        }
    }
}
