using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace NUnit2XUnit.Tests
{
    public class CasesFolderAttribute : DataAttribute
    {
        private readonly FileName FileName;

        public CasesFolderAttribute(string relativeFiles)
        {
            var fullFileName = PathResolver.Instance.Resolve(relativeFiles);
            FileName = new FileName(fullFileName);
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            IEnumerable<string> GetFiles(string path)
            {
                foreach (var file in Directory.GetFiles(path, $"{FileName.FullName}"))
                {
                    if (!Path.GetFileNameWithoutExtension(file).EndsWith(".e"))
                    {
                        yield return file;
                    }
                }

                foreach (var dir in Directory.GetDirectories(path))
                {
                    GetFiles(dir);
                }
            }

            var result = GetFiles(FileName.Path).Select(s =>
                new object[]{
                    new ConvertCase(
                        FileContent.From(s),
                        FileContent.From(ReplaceExtension(s)))
                });

            return result;
        }

        private static string ReplaceExtension(string fileName)
        {
            var f = new FileName(fileName);
            return Path.Combine(f.Path, $"{f.Name}.e{f.Extension}");
        }
    }
}
