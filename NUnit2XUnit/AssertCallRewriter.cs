using SyntaxConverters;

namespace NUnit2XUnit
{
    public class AssertCallRewriter : MethodCallNameRewriter
    {
        public AssertCallRewriter(MethodInvocation state, string method)
            : base(state, "Assert", method)
        {
        }
    }
}
