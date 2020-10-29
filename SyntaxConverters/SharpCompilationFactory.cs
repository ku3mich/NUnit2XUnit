// Copyright (C) 2019 Dmitry Yakimenko (detunized@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace SyntaxConverters
{
    public class SharpCompilationFactory
    {
        readonly string AssemblyName;

        protected SharpCompilationFactory() : this("conversion.temp") { }
        public static SharpCompilationFactory Instance { get; } = new SharpCompilationFactory();
        public SharpCompilationFactory(string assemblyName) => AssemblyName = assemblyName;

        public CSharpCompilation Create(SourceText text)
        {
            var programTree = CSharpSyntaxTree.ParseText(text, CSharpParseOptions.Default);
            var compilation = CSharpCompilation.Create(AssemblyName, new[] { programTree });
            return compilation;
        }
    }
}
