using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis.Text;
using SyntaxConverters;

namespace NUnit2XUnit
{
    public class Converter
    {
        public void Convert(string fileName)
        {
            using (var source = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var text = SourceText.From(source, Encoding.UTF8);
                var path = Path.GetDirectoryName(fileName);
                Debug.Assert(path != null);

                var compilationFactory = SharpCompilationFactory.Instance;
                var compilation = compilationFactory.Create(text);

                foreach (var sourceTree in compilation.SyntaxTrees)
                {
                    var root = sourceTree.GetRoot();
                    var newSource = Rewriter.Instance.Visit(root);
                    if (newSource != sourceTree.GetRoot())
                    {
                        File.WriteAllText(sourceTree.FilePath, newSource.ToFullString());
                    }
                }
            }
        }
    }
}
