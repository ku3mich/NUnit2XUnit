using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace SyntaxConverters
{
    public abstract class IntermediateFactoryBase<TNode, TParam> : TypedConverterFactory<TNode> where TNode : SyntaxNode
    {
        protected override ISyntaxConverter CreateConverter(TNode node)
        {
            var param = Prepare(node);
            return EqualityComparer<TParam>.Default.Equals(param, default)
                ? null
                : CreateConverter(param);
        }

        protected abstract ISyntaxConverter CreateConverter(TParam param);
        protected abstract TParam Prepare(TNode node);
    }
}
