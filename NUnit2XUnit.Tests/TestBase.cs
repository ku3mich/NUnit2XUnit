﻿using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using SyntaxConverters;
using Text.Diff;
using Xunit;
using Xunit.Abstractions;

namespace NUnit2XUnit.Tests
{
    public abstract class TestBase
    {
        protected readonly ITestOutputHelper Console;
        private readonly ITextDiff Diff;

        protected TestBase(ITestOutputHelper console, ITextDiff diff)
        {
            Console = console;
            Diff = diff;
        }

        protected HookingVisitor CreateVisitor(params (Type nodeType, Func<SyntaxNode, SyntaxNode> hook)[] hooks)
        {
            var testHooks = new SyntaxNodeHooks(hooks);
            /*testHooks.Insert(0, (typeof(SyntaxNode), s =>
            {
                if (s == null)
                {
                    return null;
                }

                Console.WriteLine($"- {s?.GetType()?.Name}");
                return s;
            }
            ));*/

            return new Rewriter(testHooks);
        }

        protected SyntaxTree Parse(string source)
        {
            var text = SourceText.From(source, Encoding.UTF8);
            var compilation = SharpCompilationFactory.Instance.Create(text);
            Assert.Single(compilation.SyntaxTrees);

            var tree = compilation.SyntaxTrees[0];
            return tree;
        }

        protected void CompareResults(string source, string expected, SyntaxNode actual) => CompareResults(source, expected, actual.ToString());

        protected void CompareResults(string source, string expected, string actual)
        {
            try
            {
                Assert.Equal(expected, actual);
            }
            catch
            {
                var diff = Diff.Generate(actual.Replace(' ', '·'), expected.Replace(' ', '·'));
                var left = diff.IndexOf('|');
                Console.WriteLine("* source");
                Console.WriteLine(source);
                Console.WriteLine($"{"= actual".PadRight(left)}+ = expected");
                Console.WriteLine(diff);

                throw;
            }
        }
    }
}
