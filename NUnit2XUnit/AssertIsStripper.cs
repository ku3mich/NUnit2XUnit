using SyntaxConverters;

namespace NUnit2XUnit
{
    public class AssertAreStripper : MethodInvocationFactoryBase
    {
        public static IConverterFactory Factory { get; } = new AssertAreStripper();

        protected override ISyntaxConverter CreateConverter(MethodInvocation state)
        {
            var s = state.IsMethodCallTo("Assert") && HasIsPrefix(state.AccessedMethod)
                // todo: eliminate such long expressions by introducing a builder + copypaste
                ? new TriviaWhitespace.Link(new AssertCallRewriter(state, state.AccessedMethod.Substring(3)), state.Node)
                : null;

            return s;
        }

        private bool HasIsPrefix(string s) => s != null && s.Length > 3 && s.StartsWith("Are", System.StringComparison.Ordinal);
    }
    public class AssertIsStripper : MethodInvocationFactoryBase
    {
        public static IConverterFactory Factory { get; } = new AssertIsStripper();

        protected override ISyntaxConverter CreateConverter(MethodInvocation state)
        {
            var s = state.IsMethodCallTo("Assert") && HasIsPrefix(state.AccessedMethod)
                // todo: eliminate such long expressions by introducing a builder
                ? new TriviaWhitespace.Link(new AssertCallRewriter(state, state.AccessedMethod.Substring(2)), state.Node)
                : null;

            return s;
        }

        private bool HasIsPrefix(string s) => s != null && s.Length > 2 && s.StartsWith("Is", System.StringComparison.Ordinal);
    }
}
