using Swiss.FCh.Cube.Dimension.Model;
using Swiss.FCh.Cube.Dimension.Services;
using Swiss.FCh.Cube.Dimension.Contract;
using Swiss.FCh.Dimension.Model;
using VDS.RDF;
using VDS.RDF.Nodes;

namespace Swiss.FCh.Cube.Dimension.Tests.Services;

internal class DimensionServiceTests
{
    private IDimensionService _dimensionService;

    [SetUp]
    public void Setup()
    {
        _dimensionService = new DimensionService();
    }

    [Test]
    public void CreateCube_WithMinimalParameters_CreatesTripleForDefinedTermSet()
    {
        using var graph = new Graph();
        graph.BaseUri = new Uri("https://politics.ld.admin.ch/apg");

        var dimensionUri = "https://politics.ld.admin.ch/apg/person";

        var triples = _dimensionService.CreateTriples(new List<DimensionItem>(), graph, dimensionUri).ToList();

        Assert.That(triples, Is.Not.Null);
        Assert.That(triples, Is.Not.Empty);

        var definedTermSet =
            triples.SingleOrDefault(x =>
                x.Predicate.AsValuedNode().AsString() == $"{RdfNamespace.Rdf.Uri}type" &&
                x.Object.AsValuedNode().AsString() == $"{RdfNamespace.Schema.Uri}/DefinedTermSet");

        Assert.That(definedTermSet, Is.Not.Null);
        Assert.That(definedTermSet.Subject.AsValuedNode().AsString(), Is.EqualTo(dimensionUri));
    }

    [Test]
    public void CreateCube_WithDefinedTermSetName_CreatesTriplesAccordingly()
    {
        using var graph = new Graph();
        graph.BaseUri = new Uri("https://politics.ld.admin.ch/apg");

        var dimensionUri = "https://politics.ld.admin.ch/apg/person";

        List<Literal> dimensionName =
            [
                new("name de", "de"),
                new("name fr", "fr")
            ];

        var triples = _dimensionService.CreateTriples(new List<DimensionItem>(), graph, dimensionUri, dimensionName).ToList();

        Assert.That(triples, Is.Not.Null);
        Assert.That(triples, Is.Not.Empty);

        var dimensionNameTriples =
            triples.Where(x =>
                x.Subject.AsValuedNode().AsString() == dimensionUri &&
                x.Predicate.AsValuedNode().AsString() == "http://schema.org/name").ToList();

        Assert.That(dimensionNameTriples, Has.Count.EqualTo(2));

        Assert.Multiple(() =>
        {
            Assert.That(dimensionNameTriples.Any(x => x.Object.AsValuedNode().AsString() == "name de"), Is.True);
            Assert.That(dimensionNameTriples.Any(x => x.Object.AsValuedNode().AsString() == "name fr"), Is.True);
        });
    }

    [Test]
    public void CreateCube_WithUriInLanguageLiteral_CreatesTriplesAccordingly()
    {
        using var graph = new Graph();
        graph.BaseUri = new Uri("https://politics.ld.admin.ch/apg");

        var dimensionUri = "https://politics.ld.admin.ch/apg/committee";

        var dimensionItem = new DimensionItem(
            1,
            new Literal("Test Value", "de"),
            new List<AdditionalLiteralProperty> {
                new("schema:url", new Literal("https://link.de", new Uri("http://www.w3.org/2001/XMLSchema#anyURI"))),
                new("schema:url", new Literal("https://link.fr", new Uri("http://www.w3.org/2001/XMLSchema#anyURI")))
            }
        );

        var triples =
            _dimensionService.CreateTriples(
                [dimensionItem],
                graph,
                dimensionUri).ToList();

        Assert.That(triples, Is.Not.Null);
        Assert.That(triples, Is.Not.Empty);

        var dimensionNameTriples =
            triples.Where(x =>
                x.Subject.AsValuedNode().AsString().Contains(dimensionUri) &&
                x.Predicate.AsValuedNode().AsString().Contains("http://schema.org/url")).ToList();

        Assert.That(dimensionNameTriples, Has.Count.EqualTo(2));

        Assert.Multiple(() =>
        {
            Assert.That(dimensionNameTriples.Any(x => x.Object.AsValuedNode().AsString() == "https://link.de"), Is.True);
            Assert.That(dimensionNameTriples.Any(x => x.Object.ToSafeString() == "https://link.de^^http://www.w3.org/2001/XMLSchema#anyURI"), Is.True);
            Assert.That(dimensionNameTriples.Any(x => x.Object.AsValuedNode().AsString() == "https://link.fr"), Is.True);
            Assert.That(dimensionNameTriples.Any(x => x.Object.ToSafeString() == "https://link.fr^^http://www.w3.org/2001/XMLSchema#anyURI"), Is.True);
        });
    }

    [Test]
    public void CreateCube_WithDimensionItemAndAdditionalProperties_CreatesTriplesAccordingly()
    {
        using var graph = new Graph();
        graph.BaseUri = new Uri("https://politics.ld.admin.ch/apg");

        var dimensionUri = "https://politics.ld.admin.ch/apg/person";

        var dimensionItem = new DimensionItem(
            1,
            new Literal("Hans Mustermann", "de"),
            new List<AdditionalLiteralProperty>{ new("schema:familyName", new Literal("Mustermann") )},
            new List<AdditionalUriProperty>{ new("schema:personGender", "schema:male") }
        );

        var triples =
            _dimensionService.CreateTriples(
                [dimensionItem],
                graph,
                dimensionUri,
                rdfTypes: ["http://schema.org/Person"]).ToList();

        Assert.That(triples, Is.Not.Null);
        Assert.That(triples, Is.Not.Empty);

        var typeTriple =
            triples.SingleOrDefault(x =>
                x.Predicate.AsValuedNode().AsString() == $"{RdfNamespace.Rdf.Uri}type" &&
                x.Object.AsValuedNode().AsString() == $"{RdfNamespace.Schema.Uri}/Person");

        Assert.That(typeTriple, Is.Not.Null);
        Assert.That(typeTriple.Subject.AsValuedNode().AsString(), Is.EqualTo($"{dimensionUri}/1"));

        var hasDefinedTermTriple = triples.SingleOrDefault(x => x.Predicate.AsValuedNode().AsString() == $"{RdfNamespace.Schema.Uri}/hasDefinedTerm");

        Assert.That(hasDefinedTermTriple, Is.Not.Null);
        Assert.That(hasDefinedTermTriple.Subject.AsValuedNode().AsString(), Is.EqualTo(dimensionUri));
        Assert.That(hasDefinedTermTriple.Object.AsValuedNode().AsString(), Is.EqualTo($"{dimensionUri}/1"));

        var inDefinedTermSetTriple = triples.SingleOrDefault(x => x.Predicate.AsValuedNode().AsString() == $"{RdfNamespace.Schema.Uri}/inDefinedTermSet");

        Assert.That(inDefinedTermSetTriple, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(inDefinedTermSetTriple.Subject.AsValuedNode().AsString(), Is.EqualTo($"{dimensionUri}/1"));
            Assert.That(inDefinedTermSetTriple.Object.AsValuedNode().AsString(), Is.EqualTo(dimensionUri));
        });

        var nameTriple =
            triples.SingleOrDefault(x =>
                x.Subject.AsValuedNode().AsString() == $"{dimensionUri}/1" &&
                x.Predicate.AsValuedNode().AsString() == $"{RdfNamespace.Schema.Uri}/name");

        Assert.That(nameTriple, Is.Not.Null);
        Assert.That(nameTriple.Object.AsValuedNode().AsString(), Is.EqualTo("Hans Mustermann"));

        var additionalLingualPropertyTriple =
            triples.SingleOrDefault(x =>
                x.Subject.AsValuedNode().AsString() == $"{dimensionUri}/1" &&
                x.Predicate.AsValuedNode().AsString() == $"{RdfNamespace.Schema.Uri}/familyName");

        Assert.That(additionalLingualPropertyTriple, Is.Not.Null);
        Assert.That(additionalLingualPropertyTriple.Object.AsValuedNode().AsString(), Is.EqualTo("Mustermann"));

        var additionalUriPropertyTriple =
            triples.SingleOrDefault(x =>
                x.Subject.AsValuedNode().AsString() == $"{dimensionUri}/1" &&
                x.Predicate.AsValuedNode().AsString() == $"{RdfNamespace.Schema.Uri}/personGender");

        Assert.That(additionalUriPropertyTriple, Is.Not.Null);
        Assert.That(additionalUriPropertyTriple.Object.AsValuedNode().AsString(), Is.EqualTo($"{RdfNamespace.Schema.Uri}/male"));
    }
}
