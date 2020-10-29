using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace SyntaxConverters
{
    using static SyntaxFactory;

    public class MethodInvocationRewriter : ConverterBase<(MethodInvocation Invocation, string Identifier, string Method)>
    {
        private readonly string Identifier;
        private readonly string Method;

        public MethodInvocationRewriter(MethodInvocation invocation, string method) : this(invocation, invocation.AccessedIdentifier, method)
        {
        }

        public MethodInvocationRewriter(MethodInvocation invocation, string identifier, string method)
            : base((invocation, identifier, method))
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
                .WithArgumentList(ArgumentList(State.Invocation.Arguments));

            return expression;
        }
    }
}
