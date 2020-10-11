// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxConverters
{
    public interface IExpressionSyntaxConverter
    {
        ExpressionSyntax Convert();
    }
}
