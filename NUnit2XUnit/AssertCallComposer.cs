// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using SyntaxConverters;

namespace NUnit2XUnit
{
    public class AssertCallComposer : MethodCallComposerBase
    {
        private readonly string _method;
        private readonly string _rewriteTo;

        public AssertCallComposer(string method, string rewriteTo)
        {
            _method = method;
            _rewriteTo = rewriteTo;
        }

        protected override IExpressionSyntaxConverter ConverterFactory(MethodCall state)
        {
            return state.IsMethodCallTo("Assert", _method) ? new MethodCallNameRewriter(state, "Assert", _rewriteTo) : null;
        }
    }
}
