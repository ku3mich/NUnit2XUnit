using Microsoft.CodeAnalysis;

namespace SyntaxConverters
{
    public interface IConverterFactory<in T>
    {
        ISyntaxConverter CreateConverter(T node);
    }

    public interface IConverterFactory : IConverterFactory<SyntaxNode>
    {
    }
}
