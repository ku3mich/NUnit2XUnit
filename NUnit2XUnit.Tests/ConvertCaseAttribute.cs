using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace NUnit2XUnit.Tests
{
    public class ConvertCaseAttribute : DataAttribute
    {
        private readonly string FileName;

        public ConvertCaseAttribute(string fileName) => FileName = fileName;

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var caseFile = PathResolver.Instance.Resolve(FileName);
            var fileName = new FileName(caseFile);

            var caseFileExpected = Path.Combine(fileName.Path, $"{fileName.Name}.e{fileName.Extension}");
            yield return new object[1]
            {
                new ConvertCase(FileContent.From(caseFile), FileContent.From(caseFileExpected))
            };
        }
    }
}
