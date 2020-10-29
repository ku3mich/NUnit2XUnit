namespace SyntaxConverters
{
    public class MemberAccessRewriteFactory : MethodInvocationRewiterBase
    {
        private readonly AccessedMember Origin;
        private readonly AccessedMember Rewrite;

        public MemberAccessRewriteFactory(AccessedMember origin, AccessedMember rewrite)
        {
            Origin = origin;
            Rewrite = rewrite;
        }

        protected override ISyntaxConverter CreateConverterInternal(MethodInvocation param)
            => new TriviaWhitespace.Link(new MethodInvocationRewriter(param, Rewrite.Identifier, Rewrite.Member), param.Node);

        protected override bool RewriteCondition(MethodInvocation param)
            => param.IsMethodCallTo(Origin.Identifier, Origin.Member);
    }
}
