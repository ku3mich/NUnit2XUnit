using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxConverters
{
    public abstract class MethodInvocationFactoryBase : IntermediateFactoryBase<InvocationExpressionSyntax, MethodInvocation>
    {
        protected override MethodInvocation Prepare(InvocationExpressionSyntax node) =>
            (node.Expression is MemberAccessExpressionSyntax) ? new MethodInvocation(node) : default;
    }
}
