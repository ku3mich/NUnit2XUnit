﻿// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Copyright (C) 2019 Dmitry Yakimenko (detunized@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NUnit2XUnit
{
    public abstract class IntermediateConverter<T> : IExpressionSyntaxConverter
    {
        public T State { get; }

        protected IntermediateConverter(T state)
        {
            State = state;
        }

        public abstract ExpressionSyntax Convert();
    }
}