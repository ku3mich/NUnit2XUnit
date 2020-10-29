using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace SyntaxConverters
{
    public class HookingVisitor : CSharpSyntaxRewriter
    {
        private readonly ILookup<Type, Func<SyntaxNode, SyntaxNode>> Hooks;
        private readonly IEnumerable<Func<SyntaxNode, SyntaxNode>> DefaultHooks;

        public HookingVisitor()
        {
        }

        public HookingVisitor(SyntaxNodeHooks hooks)
        {
            Hooks = hooks.ToLookup(s => s.Type, s => s.Hook);
            DefaultHooks = Hooks[typeof(SyntaxNode)];
        }

        public SyntaxNode VisitRoot(SyntaxTree tree) => Visit(tree.GetRoot());

        [return: NotNullIfNotNull("node")]
        public override SyntaxNode Visit(SyntaxNode node)
        {
            var processedNode = node;

            if (DefaultHooks != null)
            {
                processedNode = DefaultHooks?.Aggregate(node, (p, f) => f(p));
                if (processedNode != node)
                {
                    return Visit(processedNode);
                }
            }

            var nodeType = node?.GetType();
            if (Hooks != null)
            {
                processedNode = Hooks[nodeType].Aggregate(node, (p, f) => f(p));
                if (processedNode != node)
                {
                    return Visit(processedNode);
                }
            }

            return base.Visit(processedNode);
        }
    }
}
