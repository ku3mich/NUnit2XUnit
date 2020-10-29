using SyntaxConverters;

namespace NUnit2XUnit
{
    public class AssertIsStripper : MethodInvocationFactoryBase
    {
        public static IConverterFactory Factory { get; } = new AssertIsStripper();

        protected override ISyntaxConverter CreateConverter(MethodInvocation param)
        {
            var s = param.IsMethodCallTo("Assert") && HasIsPrefix(param.AccessedMethod)
                // todo: eliminate such long expressions by introducing a builder
                ? new TriviaWhitespace.Link(new MethodInvocationRewriter(param, param.AccessedMethod.Substring(2)), param.Node)
                : null;

            return s;
        }

        private bool HasIsPrefix(string s) => s != null && s.Length > 2 && s.StartsWith("Is", System.StringComparison.Ordinal);
    }
}
