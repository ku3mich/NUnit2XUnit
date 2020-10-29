using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxConverters
{
    public abstract class MethodInvocationFactoryBase : IntermediateFactoryBase<InvocationExpressionSyntax, MethodInvocation>
    {
        protected override MethodInvocation Prepare(InvocationExpressionSyntax param) =>
            (param.Expression is MemberAccessExpressionSyntax) ? new MethodInvocation(param) : default;
    }
}
