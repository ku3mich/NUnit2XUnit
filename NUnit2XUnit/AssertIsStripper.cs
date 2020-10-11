// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using SyntaxConverters;

namespace NUnit2XUnit
{
    public class AssertIsStripper : MethodCallComposerBase
    {
        protected override IExpressionSyntaxConverter ConverterFactory(MethodCall state)
        {
            return state.IsMethodCallTo("Assert") && IsIs(state.AccessedIdentifier) ? new AssertCallRewriter(state, state.AccessedIdentifier.Substring(2)) : null;
        }

        private bool IsIs(string s) =>
            s != null && s.Length > 2 && s.StartsWith("Is", System.StringComparison.Ordinal);
        
    }
}
