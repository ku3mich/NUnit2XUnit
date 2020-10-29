using Microsoft.CodeAnalysis;

namespace SyntaxConverters
{
    public interface ISyntaxConverter
    {
        SyntaxNode Convert();
    }

    public interface ISyntaxConverter<out T> : ISyntaxConverter
    {
        T State { get; }
    }
}
