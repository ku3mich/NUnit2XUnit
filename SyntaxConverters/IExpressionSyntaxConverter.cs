﻿// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SyntaxConverters
{
    public interface IExpressionSyntaxConverter
    {
        Task<ExpressionSyntax> Convert();
    }
}
