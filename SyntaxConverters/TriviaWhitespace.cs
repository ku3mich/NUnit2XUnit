using Microsoft.CodeAnalysis;

namespace SyntaxConverters
{
    public class TriviaWhitespace : IConverterFactory
    {
        private readonly SyntaxNode Source;

        public TriviaWhitespace(SyntaxNode source)
            => Source = source;

        public ISyntaxConverter CreateConverter(SyntaxNode node)
            => new Converter(Source, node);

        public class Converter : ChainConverter
        {
            public Converter(SyntaxNode source, SyntaxNode node)
                : base(new WhitespaceNormalize.Converter(node), new From(source))
            {
            }
        }

        public class From : IConverterFactory
        {
            private readonly SyntaxNode Source;

            public From(SyntaxNode source)
                => Source = source;

            public ISyntaxConverter CreateConverter(SyntaxNode node)
                => new Trivia(new Attach(Source, node));
        }

        public class Trivia : ConverterBase<Attach>
        {
            public override SyntaxNode Convert()
                => State.To.WithTriviaFrom(State.From);

            public Trivia(Attach state) : base(state)
            {
            }

            public Trivia(SyntaxNode from, SyntaxNode to)
                : this(new Attach(from, to))
            {
            }
        }

        public class Link : ChainConverter
        {
            public Link(ISyntaxConverter converter, SyntaxNode source)
                : base(converter, new TriviaWhitespace(source))
            {
            }
        }
    }
}
