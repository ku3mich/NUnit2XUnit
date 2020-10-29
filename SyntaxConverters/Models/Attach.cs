using Microsoft.CodeAnalysis;

namespace SyntaxConverters
{
    public readonly struct Attach
    {
        public readonly SyntaxNode From;
        public readonly SyntaxNode To;

        public Attach(SyntaxNode from, SyntaxNode to)
        {
            From = from;
            To = to;
        }
    }
}
