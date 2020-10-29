// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using System;
using Microsoft.CodeAnalysis;

namespace SyntaxConverters
{
    public class ClosureConverter : ConverterBase<Func<SyntaxNode>>
    {
        public ClosureConverter(Func<SyntaxNode> converter) : base(converter) { }
        public override SyntaxNode Convert() => State();
    }
}
