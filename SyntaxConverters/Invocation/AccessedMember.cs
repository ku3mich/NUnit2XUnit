namespace SyntaxConverters
{
    public readonly struct AccessedMember
    {
        public readonly string Identifier;
        public readonly string Member;

        public AccessedMember(string identifier, string member)
        {
            Identifier = identifier;
            Member = member;
        }
    }
}
