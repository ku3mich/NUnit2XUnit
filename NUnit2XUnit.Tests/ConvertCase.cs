using Xunit.Abstractions;

namespace NUnit2XUnit.Tests
{
    public class ConvertCase : IXunitSerializable
    {
        public FileContent Source { get; set; }
        public FileContent Expected { get; set; }

        public ConvertCase()
        {
        }

        public ConvertCase(FileContent source, FileContent expected)
        {
            Source = source;
            Expected = expected;
        }
        public override string ToString() => Source.ToString();

        public void Deserialize(IXunitSerializationInfo info)
        {
            Source = FileContent.From(info.GetValue<string>(nameof(Source)));
            Expected = FileContent.From(info.GetValue<string>(nameof(Expected)));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(Source), Source.FileName);
            info.AddValue(nameof(Expected), Expected.FileName);
        }
    }
}
