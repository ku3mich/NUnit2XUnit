using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxConverters
{
    using static SyntaxFactory;

    public class MemberAccessTemplate : ISyntaxConverter
    {
        private readonly ExpressionSyntax Identifier;
        private readonly SimpleNameSyntax Member;
        private readonly ArgumentListSyntax Args;

        protected MemberAccessTemplate(ExpressionSyntax identifier, SimpleNameSyntax member, ArgumentListSyntax args)
        {
            Identifier = identifier;
            Member = member;
            Args = args;
        }

        public SyntaxNode Convert() =>
            InvocationExpression(
                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, Identifier, Member))
            .WithArgumentList(Args);
    }
}
