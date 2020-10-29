using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SyntaxConverters;

namespace NUnit2XUnit
{
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
                Category2TraitConverter.Factory);

        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node) =>
            node.ConvertUsingOneOf(() => base.VisitInvocationExpression(node),
                AssertAreStripper.Factory,
                AssertIsStripper.Factory,
                AssertThat.Factory);

        public override SyntaxNode VisitAttributeList(AttributeListSyntax node)
        {
            var result = base.VisitAttributeList(node) as AttributeListSyntax;
            return result.Attributes.Count == 0 ? null : result;
        }
    }
}
