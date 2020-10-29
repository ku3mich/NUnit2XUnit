using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.Text;
using SyntaxConverters;

namespace NUnit2XUnit
{
    static class Program
    {
        static void ConvertFile(string inputPath)
        {
            Console.WriteLine($"* processing: {Path.GetRelativePath(".", inputPath)}");

            var module = File.ReadAllText(inputPath);
            var text = SourceText.From(module, Encoding.UTF8);
            var compilation = SharpCompilationFactory.Instance.Create(text);
            try
            {
                try
                {
                    var tree = compilation.SyntaxTrees.SingleOrDefault();
                    if (tree == null)
                    {
                        throw new Exception($"could not parse module");
                    }

                    var rewrited = Rewriter.Instance.VisitRoot(tree);
                    File.WriteAllText(inputPath, rewrited.ToString());
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("  more than one syntaxTree");
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  unable to process module due: {ex.Message}");
            };
        }

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: nunit2xunit path-to-folder");
                return;
            }

            static void ConvertDir(string dir)
            {
                foreach (var file in Directory.GetFiles(dir, "*.cs"))
                {
                    ConvertFile(file);
                }

                foreach (var file in Directory.GetDirectories(dir))
                {
                    ConvertDir(file);
                }
            }

            var path = Path.GetFullPath(args[0]);
            ConvertDir(path);
        }
    }
}
