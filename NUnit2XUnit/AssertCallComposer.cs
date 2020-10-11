﻿// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using SyntaxConverters;

namespace NUnit2XUnit
{
    public class AssertCallComposer : MethodCallComposerBase
    {
        private readonly string Method;
        private readonly string RewriteTo;

        public AssertCallComposer(string method, string rewriteTo)
        {
            Method = method;
            RewriteTo = rewriteTo;
        }

        protected override IExpressionSyntaxConverter ConverterFactory(MethodCall state)
        {
            return state.IsMethodCallTo("Assert", Method) ? new MethodCallNameRewriter(state, "Assert", RewriteTo) : null;
        }
    }
}
