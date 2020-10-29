// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using Microsoft.CodeAnalysis;

namespace SyntaxConverters
{
    public abstract class TypedConverterFactory<T> : IConverterFactory where T : SyntaxNode
    {
        public virtual ISyntaxConverter CreateConverter(SyntaxNode node) =>
            (node is T typed) ? CreateConverter(typed) : null;

        protected abstract ISyntaxConverter CreateConverter(T node);
    }
}
