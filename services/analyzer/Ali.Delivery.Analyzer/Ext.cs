using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ali.Delivery.Analyzer;

public static class Ext
{
    public static XmlElementSyntax? GetArgNullExceptionDoc(this DocumentationCommentTriviaSyntax? doc, SemanticModel semanticModel, CancellationToken cancellationToken)
    {
        return doc?.Content.GetXmlElements("exception")
                  .FirstOrDefault(s => s.StartTag.Attributes.OfType<XmlCrefAttributeSyntax>()
                                        .Any(a => semanticModel.GetSymbolInfo(a.Cref, cancellationToken)
                                                               .Symbol.IsArgNullExType()));
    }

    public static DocumentationCommentTriviaSyntax? GetDocumentationCommentTriviaSyntax(this SyntaxNode? node)
    {
        if (node == null)
        {
            return null;
        }

        foreach (var leadingTrivia in node.GetLeadingTrivia())
        {
            if (leadingTrivia.GetStructure() is DocumentationCommentTriviaSyntax structure)
            {
                return structure;
            }
        }

        return null;
    }

    public static IEnumerable<XmlNodeSyntax> GetXmlNodes(this SyntaxList<XmlNodeSyntax> content, string elementName)
    {
        foreach (var syntax in content)
        {
            switch (syntax)
            {
                case XmlEmptyElementSyntax emptyElement when Eq(elementName, emptyElement.Name):
                {
                    yield return emptyElement;
                    break;
                }
                case XmlElementSyntax elementSyntax when Eq(elementName, elementSyntax.StartTag.Name):
                {
                    yield return elementSyntax;
                    break;
                }
            }
        }

        yield break;

        static bool Eq(string elementName, XmlNameSyntax? xmlName) => string.Equals(elementName, xmlName?.ToString(), StringComparison.Ordinal);
    }

    public static bool IsArgNullExType(this ISymbol? typeSymbol) => typeSymbol is { ContainingNamespace.Name: nameof(System), Name: nameof(ArgumentNullException) };

    private static IEnumerable<XmlElementSyntax> GetXmlElements(this SyntaxList<XmlNodeSyntax> content,
                                                                string elementName,
                                                                Func<SyntaxList<XmlAttributeSyntax>, bool>? attributeFilter = null)
    {
        attributeFilter ??= _ => true;

        return content.OfType<XmlElementSyntax>()
                      .Where(s => string.Equals(elementName, s.StartTag.Name.ToString(), StringComparison.Ordinal) && attributeFilter(s.StartTag.Attributes));
    }
}
