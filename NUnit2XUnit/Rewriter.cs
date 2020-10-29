using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SyntaxConverters;

namespace NUnit2XUnit
{
    using static SyntaxFactory;

    public class Rewriter : HookingVisitor
    {
        public static Rewriter Instance { get; } = new Rewriter();

        public Rewriter()
        {
        }

        public Rewriter(SyntaxNodeHooks hooks) : base(hooks)
        {
        }

        public override SyntaxNode VisitUsingDirective(UsingDirectiveSyntax node) =>
            node.ConvertUsingOneOf(() => base.VisitUsingDirective(node),
                UsingConverter.Factory);

        public override SyntaxNode VisitAttribute(AttributeSyntax node) => node.ConvertUsingOneOf(() => base.VisitAttribute(node),
                TestFixtureStripper.Factory,
                Test2FactConverter.Factory,
                TestCase2InlineData.Factory,
                Category2TraitConverter.Factory);

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node) =>
            node.ConvertUsingOneOf(() => base.VisitInvocationExpression(node),
                AssertAreStripper.Factory,
                AssertIsStripper.Factory,
                AssertThat.Factory,
                CollectionAssert.Factory);

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var result = node;

            var attributes = node
                .AttributeLists
                .SelectMany(q => q.Attributes)
                .ToArray();

            // todo aggregate to avoid the enumeration twice

            if (attributes.Any(s => s.Name.ToString() == "TestCase") && attributes.All(s => s.Name.ToString() != "Theory"))
            {
                var theoryList = AttributeSyntaxFactory
                    .List(AttributeSyntaxFactory.Theory())
                    .NormalizeWhitespace()
                    .WithTriviaFrom(node.AttributeLists[0]);

                var attrList = node.AttributeLists
                    .Insert(0, theoryList);

                result = MethodDeclaration(
                    attrList,
                    node.Modifiers,
                    node.ReturnType,
                    node.ExplicitInterfaceSpecifier,
                    node.Identifier,
                    node.TypeParameterList,
                    node.ParameterList,
                    node.ConstraintClauses,
                    node.Body,
                    node.ExpressionBody,
                    node.SemicolonToken);
            }

            return base.VisitMethodDeclaration(result);
        }

        public override SyntaxNode VisitAttributeList(AttributeListSyntax node)
        {
            var result = base.VisitAttributeList(node) as AttributeListSyntax;
            if (result.Attributes.Count == 0)
            {
                return null;
            }

            return result;
        }
    }
}
