using SyntaxConverters;

namespace NUnit2XUnit
{
    public class AssertAreStripper : MethodInvocationFactoryBase
    {
        public static IConverterFactory Factory { get; } = new AssertAreStripper();

        protected override ISyntaxConverter CreateConverter(MethodInvocation param)
        {
            var s = param.IsMethodCallTo("Assert") && HasIsPrefix(param.AccessedMethod)
                // todo: eliminate such long expressions by introducing a builder + copypaste
                ? new TriviaWhitespace.Link(new MethodInvocationRewriter(param, param.AccessedMethod.Substring(3)), param.Node)
                : null;

            return s;
        }

        private bool HasIsPrefix(string s) => s != null && s.Length > 3 && s.StartsWith("Are", System.StringComparison.Ordinal);
    }
}
