// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Copyright (C) 2019 Dmitry Yakimenko (detunized@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

namespace NUnit2XUnit
{
    public class AssertCallRewriter : MethodCallNameRewriter
    {
        public AssertCallRewriter(MethodCallState state, string method) : base(state, "Assert", method)
        {
        }
    }
}