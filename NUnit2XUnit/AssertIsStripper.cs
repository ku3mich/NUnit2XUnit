// Copyright (C) 2020 Serhii Kuzmychov (ku3mich@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using System.Threading.Tasks;
using SyntaxConverters;

namespace NUnit2XUnit
{
    public class AssertIsStripper : MethodCallComposerBase
    {
        protected override Task<IExpressionSyntaxConverter> ConverterFactory(MethodCall state)
        {
            var s = state.IsMethodCallTo("Assert") && IsIs(state.AccessedIdentifier) ? new AssertCallRewriter(state, state.AccessedIdentifier.Substring(2)) : null;
            return Task.FromResult<IExpressionSyntaxConverter>(s);
        }

        private bool IsIs(string s)
        {
            return s != null && s.Length > 2 && s.StartsWith("Is", System.StringComparison.Ordinal);
        }
    }
}
