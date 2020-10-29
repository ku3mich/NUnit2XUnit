using SyntaxConverters;

namespace NUnit2XUnit
{
    public class AssertCallFactory : MethodInvocationFactoryBase
    {
        private readonly string Method;
        private readonly string RewriteTo;

        public AssertCallFactory(string method, string rewriteTo)
        {
            Method = method;
            RewriteTo = rewriteTo;
        }

        protected override ISyntaxConverter CreateConverter(MethodInvocation param)
        {
            var s = param.IsMethodCallTo("Assert", Method) ? new MethodCallNameRewriter(param, "Assert", RewriteTo) : null;
            return s;
        }
    }
}
