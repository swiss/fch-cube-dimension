using FCh.Cube.Dimension.Model;
using FCh.Cube.Dimension.Services;
using FCh.Cube.Dimension.Contract;
using FCh.Dimension.Model;
using VDS.RDF;
using VDS.RDF.Nodes;

namespace FCh.Cube.Dimension.Tests.Services;

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
        var graph = new Graph();
        graph.BaseUri = new Uri("https://politics.ld.admin.ch/apg");

        var dimensionUri = "https://politics.ld.admin.ch/apg/person";

        var triples = _dimensionService.CreateDimension(new List<DimensionItem>(), graph, dimensionUri).ToList();

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
        var graph = new Graph();
        graph.BaseUri = new Uri("https://politics.ld.admin.ch/apg");

        var dimensionUri = "https://politics.ld.admin.ch/apg/person";

        List<LingualLiteral> dimensionName =
            [
                new("name de", "de"),
                new("name fr", "fr")
            ];

        var triples = _dimensionService.CreateDimension(new List<DimensionItem>(), graph, dimensionUri, dimensionName).ToList();

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
    public void CreateCube_WithDimensionItemAndAdditionalProperties_CreatesTriplesAccordingly()
    {
        var graph = new Graph();
        graph.BaseUri = new Uri("https://politics.ld.admin.ch/apg");

        var dimensionUri = "https://politics.ld.admin.ch/apg/person";

        var dimensionItem = new DimensionItem(
            1,
            new LingualLiteral("Hans Mustermann", "de"),
            new List<AdditionalLingualProperty>{ new("schema:familyName", new LingualLiteral("Mustermann") )},
            new List<AdditionalUriProperty>{ new("schema:personGender", "schema:male") }
        );

        var triples =
            _dimensionService.CreateDimension(
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
