using Bk.Cube.Dimension.Model;
using Bk.Dimension.Contract;
using Bk.Dimension.Model;
using VDS.RDF;

namespace Bk.Cube.Dimension.Services;

internal class DimensionService : IDimensionService
{
    public IEnumerable<Triple> CreateDimension(
        IEnumerable<DimensionItem> items,
        Graph graph,
        string dimensionUri,
        IList<LingualLiteral>? dimensionName = null,
        IList<RdfNamespace>? additionalRdfNamespaces = null,
        IList<string>? rdfTypes = null)
    {
        AddRdfNamespaces(graph, additionalRdfNamespaces);

        yield return CreateDefinedTermSet(graph, dimensionUri);

        foreach (var name in dimensionName ?? Enumerable.Empty<LingualLiteral>())
        {
            yield return CreateDefinedTermSetName(graph, dimensionUri, name);
        }

        foreach (var item in items)
        {
            foreach (var rdfType in rdfTypes ?? Enumerable.Empty<string>())
            {
                yield return CreateRdfType(graph, dimensionUri, rdfType, item);
            }

            yield return CreateHasDefinedTermSet(graph, dimensionUri, item);

            yield return CreateInDefinedTermSet(graph, dimensionUri, item);

            yield return CreateSchemaName(graph, dimensionUri, item);

            foreach (var additionalProperty in item.AdditionalLingualProperties)
            {
                yield return CreateTripleForAdditionalLingualProperty(graph, dimensionUri, additionalProperty, item);
            }

            foreach (var additionalUriProperty in item.AdditionalUriProperties)
            {
                yield return CreateTripleForAdditionalUriProperty(graph, dimensionUri, additionalUriProperty, item);
            }
        }
    }

    private static Triple CreateTripleForAdditionalLingualProperty(
        Graph graph,
        string dimensionUri,
        AdditionalLingualProperty additionalProperty,
        DimensionItem item)
    {
        var obj =
            additionalProperty.Object.LanguageTag != null
                ? graph.CreateLiteralNode(additionalProperty.Object.Text, additionalProperty.Object.LanguageTag)
                : graph.CreateLiteralNode(additionalProperty.Object.Text);

        var additionalPropertyTriple = new Triple(
            graph.CreateUriNode(new Uri($"{dimensionUri}/{item.Key}")),
            graph.CreateUriNode(additionalProperty.Predicate),
            obj);
        return additionalPropertyTriple;
    }

    private static Triple CreateTripleForAdditionalUriProperty(
        Graph graph,
        string dimensionUri,
        AdditionalUriProperty additionalProperty,
        DimensionItem item)
    {
        var additionalPropertyTriple = new Triple(
            graph.CreateUriNode(new Uri($"{dimensionUri}/{item.Key}")),
            graph.CreateUriNode(additionalProperty.Predicate),
            graph.CreateUriNode(additionalProperty.Object));

        return additionalPropertyTriple;
    }

    private static Triple CreateSchemaName(Graph graph, string dimensionUri, DimensionItem item)
    {
        return new Triple(
            graph.CreateUriNode(new Uri($"{dimensionUri}/{item.Key}")),
            graph.CreateUriNode("schema:name"),
            graph.CreateLiteralNode(item.Name.Text, item.Name.LanguageTag));
    }

    private static Triple CreateInDefinedTermSet(Graph graph, string dimensionUri, DimensionItem item)
    {
        return new Triple(
            graph.CreateUriNode(new Uri($"{dimensionUri}/{item.Key}")),
            graph.CreateUriNode("schema:inDefinedTermSet"),
            graph.CreateUriNode(new Uri(dimensionUri)));
    }

    private static Triple CreateHasDefinedTermSet(Graph graph, string dimensionUri, DimensionItem item)
    {
        return new Triple(
            graph.CreateUriNode(new Uri(dimensionUri)),
            graph.CreateUriNode("schema:hasDefinedTerm"),
            graph.CreateUriNode(new Uri($"{dimensionUri}/{item.Key}")));
    }

    private static Triple CreateRdfType(Graph graph, string dimensionUri, string rdfType, DimensionItem item)
    {
        return new Triple(
            graph.CreateUriNode(new Uri($"{dimensionUri}/{item.Key}")),
            graph.CreateUriNode("rdf:type"),
            graph.CreateUriNode(new Uri(rdfType)));
    }

    private static Triple CreateDefinedTermSetName(Graph graph, string dimensionUri, LingualLiteral name)
    {
        var obj =
            name.LanguageTag != null
                ? graph.CreateLiteralNode(name.Text, name.LanguageTag)
                : graph.CreateLiteralNode(name.Text);

        var definedTermSetName = new Triple(
            graph.CreateUriNode(new Uri(dimensionUri)),
            graph.CreateUriNode("schema:name"),
            obj);

        return definedTermSetName;
    }

    private static Triple CreateDefinedTermSet(Graph graph, string dimensionUri)
    {
        return new Triple(
            graph.CreateUriNode(new Uri(dimensionUri)),
            graph.CreateUriNode("rdf:type"),
            graph.CreateUriNode("schema:DefinedTermSet"));
    }

    private static void AddRdfNamespaces(Graph graph, IList<RdfNamespace>? additionalRdfNamespaces)
    {
        AddRdfNamespace(graph, RdfNamespace.Rdf);
        AddRdfNamespace(graph, RdfNamespace.Schema);

        foreach (var rdfNamespace in additionalRdfNamespaces ?? Enumerable.Empty<RdfNamespace>())
        {
            AddRdfNamespace(graph, rdfNamespace);
        }
    }

    private static void AddRdfNamespace(Graph graph, RdfNamespace rdfNamespace)
    {
        graph.NamespaceMap.AddNamespace(rdfNamespace.Prefix, new Uri(rdfNamespace.Uri));
    }
}
