using Microsoft.CodeAnalysis;

namespace SyntaxConverters
{
    public abstract class ConverterBase<T> : ISyntaxConverter<T>
    {
        public T State { get; }
        protected ConverterBase(T state) => State = state;
        public abstract SyntaxNode Convert();
    }
}
