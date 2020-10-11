// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxConverters
{
    public abstract class ConverterBase<T> : IExpressionSyntaxConverter
    {
        public T State { get; }

        protected ConverterBase(T state)
        {
            State = state;
        }

        public abstract ExpressionSyntax Convert();
    }
}
