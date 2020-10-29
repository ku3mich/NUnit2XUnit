using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace SyntaxConverters
{
    public class SyntaxNodeHooks : List<(Type Type, Func<SyntaxNode, SyntaxNode> Hook)>
    {
        public SyntaxNodeHooks()
        {
        }

        public SyntaxNodeHooks(IEnumerable<(Type Type, Func<SyntaxNode, SyntaxNode> Hook)> hooks) : base(hooks) { }
    }
}
