// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Copyright (C) 2019 Dmitry Yakimenko (detunized@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

namespace NUnit2XUnit
{
    public class AssertIsStripper : MethodCallComposerBase
    {
        protected override IExpressionSyntaxConverter ConverterFactory(MethodCallState state)
        {
            return state.IsMethodCallTo("Assert") && IsIs(state.AccessedIdentifier) ? new AssertCallRewriter(state, state.AccessedIdentifier.Substring(2)) : null;
        }

        private bool IsIs(string s)
        {
            return s != null && s.Length > 2 && s.StartsWith("Is", System.StringComparison.Ordinal);
        }
    }
}