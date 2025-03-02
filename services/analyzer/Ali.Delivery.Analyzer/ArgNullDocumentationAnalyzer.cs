using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Ali.Delivery.Analyzer;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class ArgNullDocumentationAnalyzer : DiagnosticAnalyzer
{
    public const string DocInvalidId = "AIR0002";
    public const string DocMissingId = "AIR0001";
    public const string DocMissingParamId = "AIR0003";
    private const string Category = "Documentation";

    internal static readonly DiagnosticDescriptor DocMissingRule = new(DocMissingId,
                                                                       $"Отсутствует документация для {nameof(ArgumentNullException)}",
                                                                       $"Отсутствует документация для {nameof(ArgumentNullException)}",
                                                                       Category,
                                                                       DiagnosticSeverity.Warning,
                                                                       true);

    internal static readonly DiagnosticDescriptor DocInvalidRule = new(DocInvalidId,
                                                                       $"Не верный формат документации для {nameof(ArgumentNullException)}",
                                                                       $"Документация для {nameof(ArgumentNullException)} не соответствует ожидаемому формату",
                                                                       Category,
                                                                       DiagnosticSeverity.Warning,
                                                                       true);

    internal static readonly DiagnosticDescriptor DocMissingParamRule = new(DocMissingParamId,
                                                                            $"В документации для {nameof(ArgumentNullException)} пропущен параметр",
                                                                            $"В документации для {nameof(ArgumentNullException)} пропущен один из параметров параметр",
                                                                            Category,
                                                                            DiagnosticSeverity.Warning,
                                                                            true);

    // Keep in mind: you have to list your rules here.
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(DocMissingRule, DocInvalidRule, DocMissingParamRule);

    public override void Initialize(AnalysisContext context)
    {
        // You must call this method to avoid analyzing generated code.
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

        // You must call this method to enable the Concurrent Execution.
        context.EnableConcurrentExecution();

        context.RegisterSymbolStartAction(ProcessMethod, SymbolKind.Method);
    }

    private static void ProcessMethod(SymbolStartAnalysisContext ctx)
    {
        if (ctx.Symbol is not { Kind: SymbolKind.Method, DeclaredAccessibility: Accessibility.Public or Accessibility.Internal or Accessibility.Protected })
        {
            return;
        }

        if (ctx.Symbol is not IMethodSymbol { Parameters.Length: > 0, MethodKind: MethodKind.Constructor or MethodKind.Ordinary or MethodKind.Conversion } methodSymbol)
        {
            return;
        }

        var checkedParams = methodSymbol.Parameters.Where(p => p is { NullableAnnotation: not NullableAnnotation.Annotated, Type.IsReferenceType: true })
                                        .ToList();

        if (checkedParams.Count == 0)
        {
            return;
        }

        var state = new AnalysisState(checkedParams.Select(p => new AnalysisState.Arg(p.Name, p.Locations.FirstOrDefault() ?? Location.None)));

        ctx.RegisterSyntaxNodeAction(state.CollectDocParams, SyntaxKind.MethodDeclaration, SyntaxKind.ConstructorDeclaration, SyntaxKind.ConversionOperatorDeclaration);
        ctx.RegisterOperationAction(state.CollectThrownParams, OperationKind.Throw);
        ctx.RegisterOperationAction(state.CollectThrownWithHelper, OperationKind.Invocation);

        ctx.RegisterSymbolEndAction(state.CompleteReport);
    }
}

public class AnalysisState(IEnumerable<AnalysisState.Arg> paramNames)
{
    private static readonly Regex ValidDocRegex = new("""^Возникает,\s+если\s+(<paramref\s+name="[^"]+"\s*/>\s+или\s+)*<paramref\s+name="[^"]+"\s*/>\s+равен\s+<c>null</c>\.$""",
                                                      RegexOptions.Compiled);

    private Location? _docLocation;
    private readonly HashSet<string> _docThrownParams = [];
    private DiagnosticDescriptor? _invalidDocState;
    private readonly List<Arg> _params = paramNames.ToList();
    private readonly ConcurrentBag<Arg> _thrownParams = [];

    public void CollectDocParams(SyntaxNodeAnalysisContext c)
    {
        var node = c.Node;

        if (node.Kind() is not (SyntaxKind.MethodDeclaration or SyntaxKind.ConstructorDeclaration or SyntaxKind.ConversionOperatorDeclaration))
        {
            return;
        }

        var semanticModel = c.SemanticModel;

        var doc = node.GetDocumentationCommentTriviaSyntax();
        var exceptionDoc = doc?.GetArgNullExceptionDoc(semanticModel, c.CancellationToken);

        if (exceptionDoc is null)
        {
            var location = node switch
            {
                ConstructorDeclarationSyntax n => n.Identifier.GetLocation(),
                MethodDeclarationSyntax n => n.Identifier.GetLocation(),
                ConversionOperatorDeclarationSyntax n => n.Type.GetLocation(),
                _ => node.GetLocation()
            };

            SetDocLocation(location);
            SetDocInvalid(ArgNullDocumentationAnalyzer.DocMissingRule);
            return;
        }

        SetDocLocation(exceptionDoc.GetLocation());

        var contentLines = exceptionDoc.Content.Select(x => x.ToString());

        var content = string.Join("", contentLines)
                            .Trim();

        var docString = Regex.Replace(content, "(^ */// *)", "", RegexOptions.Multiline)
                             .Trim();

        if (!ValidDocRegex.IsMatch(docString))
        {
            SetDocInvalid(ArgNullDocumentationAnalyzer.DocInvalidRule);
            return;
        }

        var args = exceptionDoc.Content.GetXmlNodes("paramref")
                               .Select(GetFirstAttributeOrDefault<XmlNameAttributeSyntax>)
                               .Where(n => n is not null)
                               .Select(n => n!.Identifier.ToString());

        foreach (var s in args)
        {
            _docThrownParams.Add(s);
        }
    }

    public void CollectThrownParams(OperationAnalysisContext c)
    {
        if (c.Operation is not IThrowOperation { SemanticModel: { } sm, Syntax: { } syntax })
        {
            return;
        }

        var ex = syntax switch
        {
            ThrowExpressionSyntax s => s.Expression,
            ThrowStatementSyntax s => s.Expression,
            _ => null
        };

        if (ex is not ObjectCreationExpressionSyntax objCre)
        {
            return;
        }

        var operation = sm.GetOperation(objCre, c.CancellationToken);

        if (operation is not IObjectCreationOperation objCreOp || !objCreOp.Type.IsArgNullExType())
        {
            return;
        }

        var paramName = GetArgNullParamName(objCreOp.Arguments);

        if (!string.IsNullOrWhiteSpace(paramName))
        {
            AddThrownParam(paramName!, ex.GetLocation());
        }
    }

    public void CollectThrownWithHelper(OperationAnalysisContext c)
    {
        if (c.Operation is not IInvocationOperation op)
        {
            return;
        }

        if (!op.TargetMethod.ReceiverType.IsArgNullExType() || op.TargetMethod.Name != "ThrowIfNull")
        {
            return;
        }

        var arguments = op.Arguments;
        var paramName = GetArgNullParamName(arguments);

        if (!string.IsNullOrWhiteSpace(paramName))
        {
            AddThrownParam(paramName!, op.Syntax.GetLocation());
        }
    }

    public void CompleteReport(SymbolAnalysisContext c)
    {
        var thrownParams = _params.Join(_thrownParams, arg => arg.ParamName, arg => arg.ParamName, (arg, _) => arg.ParamName)
                                  .ToList();

        if (thrownParams.Count == 0)
        {
            return;
        }

        if (_invalidDocState is not null)
        {
            c.ReportDiagnostic(CreateDiag(_invalidDocState, thrownParams));
            return;
        }

        if (!_docThrownParams.SetEquals(thrownParams))
        {
            c.ReportDiagnostic(CreateDiag(ArgNullDocumentationAnalyzer.DocMissingParamRule, thrownParams));
        }
    }

    private void AddThrownParam(string paramName, Location location)
    {
        _thrownParams.Add(new Arg(paramName, location));
    }

    private Diagnostic CreateDiag(DiagnosticDescriptor rule, IEnumerable<string> paramNames)
    {
        var props = ImmutableDictionary<string, string?>.Empty.Add("args", string.Join(",", paramNames));
        return Diagnostic.Create(rule, _docLocation, props);
    }

    private static string? GetArgNullParamName(ImmutableArray<IArgumentOperation> arguments)
    {
        var pn = arguments.FirstOrDefault(a => a.Parameter?.Name == "paramName");
        return pn is { Value.ConstantValue: { HasValue: true, Value: string argValue } } ? argValue : null;
    }

    private static T? GetFirstAttributeOrDefault<T>(XmlNodeSyntax nodeSyntax) where T : XmlAttributeSyntax
    {
        return nodeSyntax switch
        {
            XmlEmptyElementSyntax emptyElementSyntax => emptyElementSyntax.Attributes.OfType<T>()
                                                                          .FirstOrDefault(),
            XmlElementSyntax elementSyntax => elementSyntax.StartTag.Attributes.OfType<T>()
                                                           .FirstOrDefault(),
            _ => null
        };
    }

    private void SetDocInvalid(DiagnosticDescriptor rule) => _invalidDocState = rule;

    private void SetDocLocation(Location location) => _docLocation = location;

    public readonly record struct Arg(string ParamName, Location Location)
    {
        public Location Location { get; } = Location;
        public string ParamName { get; } = ParamName;
    }
}
