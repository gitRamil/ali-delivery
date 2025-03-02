using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ali.Delivery.Analyzer;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ArgNullDocumentationCodeFixProvider))]
[Shared]
public class ArgNullDocumentationCodeFixProvider : CodeFixProvider
{
    private const string FixActionTitle = "Исправить документацию для ArgumentNullException";

    // Specify the diagnostic IDs of analyzers that are expected to be linked.
    public sealed override ImmutableArray<string> FixableDiagnosticIds { get; } =
        ImmutableArray.Create(ArgNullDocumentationAnalyzer.DocMissingId, ArgNullDocumentationAnalyzer.DocInvalidId, ArgNullDocumentationAnalyzer.DocMissingParamId);

    // If you don't need the 'fix all' behaviour, return null.
    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        // We link only one diagnostic and assume there is only one diagnostic in the context.
        var diagnostic = context.Diagnostics.Single();

        // 'SourceSpan' of 'Location' is the highlighted area. We're going to use this area to find the 'SyntaxNode' to rename.
        var diagnosticSpan = diagnostic.Location.SourceSpan;

        // Get the root of Syntax Tree that contains the highlighted diagnostic.
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken)
                                .ConfigureAwait(false);

        // Find SyntaxNode corresponding to the diagnostic.
        var diagnosticNode = root?.FindNode(diagnosticSpan);

        // conversion operator method extraction
        if (diagnosticNode is TypeSyntax typeSyntax)
        {
            diagnosticNode = typeSyntax.Parent;
        }

        // To get the required metadata, we should match the Node to the specific type: 'ClassDeclarationSyntax'.
        if (diagnosticNode is not BaseMethodDeclarationSyntax declaration)
        {
            return;
        }

        // Register a code action that will invoke the fix.
        context.RegisterCodeFix(CodeAction.Create(FixActionTitle,
                                                  equivalenceKey: FixActionTitle,
                                                  createChangedDocument: async _ =>
                                                  {
                                                      await Task.CompletedTask;

                                                      var doc = context.Document;

                                                      var props = diagnostic.Properties;

                                                      if (!props.TryGetValue("args", out var args) || string.IsNullOrWhiteSpace(args))
                                                      {
                                                          return doc;
                                                      }

                                                      var split = args!.Split(',');
                                                      var rw = new MyRewriter(split, declaration);
                                                      var upd = rw.Visit(root);
                                                      var newDoc = doc.WithSyntaxRoot(upd!);
                                                      return newDoc;
                                                  }),
                                diagnostic);
    }

    private class MyRewriter(IEnumerable<string> args, BaseMethodDeclarationSyntax methodDeclarationSyntax) : CSharpSyntaxRewriter
    {
        private readonly List<string> _args = args.ToList();

        public override SyntaxNode? VisitConstructorDeclaration(ConstructorDeclarationSyntax node) =>
            node == methodDeclarationSyntax ? FixDoc(node) : base.VisitConstructorDeclaration(node);

        public override SyntaxNode? VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node) =>
            node == methodDeclarationSyntax ? FixDoc(node) : base.VisitConversionOperatorDeclaration(node);

        public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node) => node == methodDeclarationSyntax ? FixDoc(node) : base.VisitMethodDeclaration(node);

        private SyntaxNode FixDoc(BaseMethodDeclarationSyntax node)
        {
            var rw1 = new Rw1(_args);

            var doc = node.GetDocumentationCommentTriviaSyntax();

            if (doc is not null)
            {
                return rw1.Visit(node);
            }

            var ts = SyntaxFactory.IdentifierName("ArgumentNullException");
            var exDoc = SyntaxFactory.XmlExceptionElement(SyntaxFactory.TypeCref(ts), ArgNullDocGenerator.GetContent(_args));
            var doc1 = SyntaxFactory.DocumentationComment(exDoc);
            return node.WithLeadingTrivia(SyntaxFactory.ElasticCarriageReturnLineFeed, SyntaxFactory.Trivia(doc1), SyntaxFactory.ElasticCarriageReturnLineFeed);
        }

        private class ArgNullDocGenerator
        {
            public static SyntaxList<XmlNodeSyntax> GetContent(IReadOnlyList<string> args) => SyntaxFactory.List(GetNodes(args));

            private static IEnumerable<XmlNodeSyntax> GetNodes(IReadOnlyList<string> args)
            {
                var separatorText = SyntaxFactory.XmlText(" или ");

                yield return SyntaxFactory.XmlNewLine("\r\n");

                yield return SyntaxFactory.XmlText("Возникает, если ");

                yield return SyntaxFactory.XmlParamRefElement(args[0]);

                for (var i = 1; i < args.Count; i++)
                {
                    yield return separatorText;
                    yield return SyntaxFactory.XmlParamRefElement(args[i]);
                }

                yield return SyntaxFactory.XmlText(" равен ");

                yield return SyntaxFactory.XmlElement("c",
                                                      SyntaxFactory.List(new XmlNodeSyntax[]
                                                      {
                                                          SyntaxFactory.XmlText("null")
                                                      }));
                yield return SyntaxFactory.XmlText(".");
                yield return SyntaxFactory.XmlNewLine("\r\n");
            }
        }

        private class Rw1(List<string> args) : CSharpSyntaxRewriter(true)
        {
            private bool _found;

            public override SyntaxNode? VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax node)
            {
                var upd = base.VisitDocumentationCommentTrivia(node);

                if (_found)
                {
                    return upd;
                }

                var ts = SyntaxFactory.IdentifierName("ArgumentNullException");

                var exDoc = SyntaxFactory.XmlExceptionElement(SyntaxFactory.TypeCref(ts), ArgNullDocGenerator.GetContent(args))
                                         .WithLeadingTrivia(SyntaxFactory.DocumentationCommentExterior("/// "));

                return node.AddContent(exDoc)
                           .WithTrailingTrivia(SyntaxFactory.ElasticCarriageReturnLineFeed);
            }

            public override SyntaxNode VisitXmlElement(XmlElementSyntax node)
            {
                if (node.StartTag.Name.LocalName.Text != "exception")
                {
                    return node;
                }

                if (GetCrefName(node) != nameof(ArgumentNullException))
                {
                    return node;
                }

                _found = true;

                var list = ArgNullDocGenerator.GetContent(args);
                return node.WithContent(list);
            }

            private static string? GetCrefName(XmlElementSyntax node)
            {
                var crefAttributeSyntax = node.StartTag.Attributes.OfType<XmlCrefAttributeSyntax>()
                                              .FirstOrDefault();

                var member = crefAttributeSyntax?.Cref switch
                {
                    QualifiedCrefSyntax qc => qc.Member.ToString(),
                    TypeCrefSyntax tc => tc.Type.ToString(),
                    NameMemberCrefSyntax mc => mc.Name.ToString(),
                    _ => null
                };

                return member;
            }
        }
    }
}
