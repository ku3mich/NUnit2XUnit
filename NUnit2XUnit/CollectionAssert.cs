using SyntaxConverters;

namespace NUnit2XUnit
{
    public class CollectionAssert
    {
        public static IConverterFactory Factory { get; } = new MemberAccessRewriteFactory(
            new AccessedMember("CollectionAssert", "AreEquivalent"),
            new AccessedMember("Assert", "Equal"));
    }
}
