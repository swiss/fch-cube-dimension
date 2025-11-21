using System.Diagnostics.CodeAnalysis;

namespace Swiss.FCh.Cube.Dimension.Model;

public class DimensionItem
{
    public required object Key { get; init; }

    public required Literal Name { get; init; }

    public List<AdditionalLiteralProperty> AdditionalLiteralProperties { get; } = new List<AdditionalLiteralProperty>();

    public List<AdditionalUriProperty> AdditionalUriProperties { get; } = new List<AdditionalUriProperty>();

    [SetsRequiredMembers]
    public DimensionItem(
        object key,
        Literal name,
        IList<AdditionalLiteralProperty>? additionalProperties = null,
        IList<AdditionalUriProperty>? additionalUriProperties = null)
    {
        Key = key;
        Name = name;
        AdditionalLiteralProperties.AddRange(additionalProperties ?? Enumerable.Empty<AdditionalLiteralProperty>());
        AdditionalUriProperties.AddRange(additionalUriProperties ?? Enumerable.Empty<AdditionalUriProperty>());
    }
}

public class AdditionalLiteralProperty
{
    public required string Predicate { get; init; }

    public required Literal Object  { get; init; }

    [SetsRequiredMembers]
    public AdditionalLiteralProperty(string predicate, Literal obj)
    {
        Predicate = predicate;
        Object = obj;
    }
}

public class AdditionalUriProperty
{
    public required string Predicate { get; init; }

    public required string Object { get; init; }

    [SetsRequiredMembers]
    public AdditionalUriProperty(string predicate, string obj)
    {
        Predicate = predicate;
        Object = obj;
    }
}

public class Literal
{
    public required string Text { get; init; }

    public string? LanguageTag { get; init; }

    public Uri? DataType { get; init; }

    [SetsRequiredMembers]
    public Literal(string text, string? languageTag = null)
    {
        Text = text;
        LanguageTag = languageTag;
    }

    [SetsRequiredMembers]
    public Literal(string text, Uri dataType)
    {
        Text = text;
        DataType = dataType;
    }
}
