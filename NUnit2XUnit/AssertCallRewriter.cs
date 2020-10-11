// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using SyntaxConverters;

namespace NUnit2XUnit
{
    public class AssertCallRewriter : MethodCallNameRewriter
    {
        public AssertCallRewriter(MethodCall state, string method) : base(state, "Assert", method)
        {
        }
    }
}
