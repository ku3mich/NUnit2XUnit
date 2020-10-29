using Microsoft.CodeAnalysis;

namespace SyntaxConverters
{
    public class WhitespaceNormalize : IConverterFactory
    {
        public static IConverterFactory Factory { get; } = new WhitespaceNormalize();
        public ISyntaxConverter CreateConverter(SyntaxNode node) => new Converter(node);

        public class Converter : ConverterBase<SyntaxNode>
        {
            public Converter(SyntaxNode node) : base(node)
            {
            }

            public override SyntaxNode Convert() => State.NormalizeWhitespace();
        }
    }
}
