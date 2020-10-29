using Microsoft.CodeAnalysis;

namespace SyntaxConverters
{
    public class ChainConverter : ISyntaxConverter
    {
        private readonly ISyntaxConverter Converter;
        private readonly IConverterFactory Factory;

        public ChainConverter(ISyntaxConverter converter, IConverterFactory factory)
        {
            Converter = converter;
            Factory = factory;
        }

        public SyntaxNode Convert()
            => Factory.CreateConverter(Converter.Convert()).Convert();
    }
}
