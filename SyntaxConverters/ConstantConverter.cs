using Microsoft.CodeAnalysis;

namespace SyntaxConverters
{
    public class ConstantConverter : ClosureConverter
    {
        public ConstantConverter(SyntaxNode node) : base(() => node) { }
        public static ISyntaxConverter Null { get; } = new ConstantConverter(null);
    }
}
