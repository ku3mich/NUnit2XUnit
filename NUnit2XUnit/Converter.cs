// Copyright (C) 2019 Dmitry Yakimenko (detunized@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace NUnit2XUnit
{
    public class Converter
    {
        public void Convert(string fileName, Stream destination)
        {
            using (FileStream source = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                SourceText text = SourceText.From(source, Encoding.UTF8);
                string path = Path.GetDirectoryName(fileName);
                Debug.Assert(path != null);

                Microsoft.CodeAnalysis.SyntaxTree programTree = CSharpSyntaxTree.ParseText(text, CSharpParseOptions.Default, path);
                CSharpCompilation compilation = CSharpCompilation.Create("nunit2xunit.conversion.temp", new[] { programTree });

                foreach (Microsoft.CodeAnalysis.SyntaxTree sourceTree in compilation.SyntaxTrees)
                {
                    Microsoft.CodeAnalysis.SyntaxNode root = sourceTree.GetRoot();
                    Microsoft.CodeAnalysis.SyntaxNode newSource = NunitToXunitRewriter.Instance.Visit(root);
                    if (newSource != sourceTree.GetRoot())
                        File.WriteAllText(sourceTree.FilePath, newSource.ToFullString());
                }
            }

        }
    }
}