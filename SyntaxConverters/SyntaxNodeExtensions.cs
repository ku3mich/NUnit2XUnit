using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace SyntaxConverters
{
    public static class SyntaxNodeExtensions
    {
        public static SyntaxNode ConvertUsingOneOf(this SyntaxNode node, Func<SyntaxNode> fallback, params IConverterFactory[] factories) =>
            node.ConvertUsingOneOf(fallback, (IEnumerable<IConverterFactory>)factories);

        public static SyntaxNode ConvertUsingOneOf(this SyntaxNode node, Func<SyntaxNode> fallback, IEnumerable<IConverterFactory> factories)
        {
            var converter = node.PickupConverter(factories);
            return converter == null ? fallback() : converter.Convert();
        }

        public static ISyntaxConverter PickupConverter(this SyntaxNode node, IEnumerable<IConverterFactory> factories)
        {
            if (node == null)
            {
                return null;
            }

            var converter = factories
                .Select(s => s.CreateConverter(node))
                .FirstOrDefault(s => s != null);

            return converter;
        }

        public static ISyntaxConverter PickupConverter(this SyntaxNode node, params IConverterFactory[] factories)
            => node.PickupConverter((IEnumerable<IConverterFactory>)factories);
    }
}
