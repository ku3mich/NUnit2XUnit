using Microsoft.CodeAnalysis;
using SyntaxConverters;

namespace NUnit2XUnit
{
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

    public class XUnitUsingConverter : ISyntaxConverter
    {
        private readonly string Namespace;

        public static ISyntaxConverter Instance { get; } = new XUnitUsingConverter("Xunit");

        protected XUnitUsingConverter(string @namespace) => Namespace = @namespace;

        public SyntaxNode Convert() => UsingDirective(IdentifierName(Namespace));
    }
}
