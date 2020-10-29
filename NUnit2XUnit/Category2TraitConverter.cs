using Microsoft.CodeAnalysis.CSharp.Syntax;
using SyntaxConverters;

namespace NUnit2XUnit
{
    public class Category2TraitConverter : TypedConverterFactory<AttributeSyntax>
    {
        public static IConverterFactory Factory { get; } = new Category2TraitConverter();

        protected override ISyntaxConverter CreateConverter(AttributeSyntax node)
        {
            if (node.Name?.ToString() != "Category")
            {
                return null;
            }

            var categoryName = node.ArgumentList?.Arguments.FirstOrDefault().Expression;

            if (categoryName == null)
            {
                return null;
            }

            var result = new TriviaWhitespace.Link(
                new ConstantConverter(AttributeSyntaxFactory.Category(categoryName)),
                node);

            return result;
        }
    }
}
