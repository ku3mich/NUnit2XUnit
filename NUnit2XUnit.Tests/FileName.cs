namespace NUnit2XUnit.Tests
{
    public readonly struct FileName
    {
        public readonly string Path;
        public readonly string Name;
        public readonly string Extension;
        public readonly string FullName;

        public FileName(string fullFileName)
        {
            Extension = System.IO.Path.GetExtension(fullFileName);
            Name = System.IO.Path.GetFileNameWithoutExtension(fullFileName);
            Path = System.IO.Path.GetDirectoryName(fullFileName);
            FullName = $"{Name}{Extension}";
        }
    }
}
