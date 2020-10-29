using System.IO;
using Xunit;


namespace NUnit2XUnit.Tests
{
    public class FileContent
    {
        private static readonly string Root = PathResolver.Instance.Resolve("");

        public string FileName { get; set; }
        public string Content { get; set; }

        public override string ToString() => MakeRelativeFileName(FileName);

        private string MakeRelativeFileName(string fileName) => fileName.Replace(Root, "").Substring(1);

        public FileContent()
        {
        }

        public FileContent(string fileName)
        {
            Content = File.ReadAllText(fileName);
            FileName = fileName;
        }

        public static FileContent From(string fileName) => new FileContent(fileName);

        public static implicit operator string(FileContent f) => f.Content;
    }
}
