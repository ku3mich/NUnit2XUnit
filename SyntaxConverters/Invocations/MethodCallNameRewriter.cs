using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace SyntaxConverters
{
    using static SyntaxFactory;

    public class MethodCallNameRewriter : ConverterBase<MethodInvocation>
    {
        private readonly string Identifier;
        private readonly string Method;

        public MethodCallNameRewriter(MethodInvocation state, string identifier, string method)
            : base(state)
        {
            Identifier = identifier;
            Method = method;
        }

        public override SyntaxNode Convert()
        {
            var expression = InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(Identifier),
                        IdentifierName(Method)))
                .WithArgumentList(ArgumentList(State.Arguments));

            return expression;
        }
    }
}
