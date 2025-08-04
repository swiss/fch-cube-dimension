using System.Diagnostics.CodeAnalysis;
using Lucene.Net.Util;

namespace Bk.Cube.Dimension.Model;

public class DimensionItem
{
    public required object Key { get; init; }

    public required LingualLiteral Name { get; init; }

    public IList<AdditionalLingualProperty> AdditionalLingualProperties { get; } = new List<AdditionalLingualProperty>();

    public IList<AdditionalUriProperty> AdditionalUriProperties { get; } = new List<AdditionalUriProperty>();

    [SetsRequiredMembers]
    public DimensionItem(
        object key,
        LingualLiteral name,
        IList<AdditionalLingualProperty>? additionalProperties = null,
        IList<AdditionalUriProperty>? additionalUriProperties = null)
    {
        Key = key;
        Name = name;
        AdditionalLingualProperties.AddRange(additionalProperties ?? Enumerable.Empty<AdditionalLingualProperty>());
        AdditionalUriProperties.AddRange(additionalUriProperties ?? Enumerable.Empty<AdditionalUriProperty>());
    }
}

public class AdditionalLingualProperty
{
    public required string Predicate { get; init; }

    public required LingualLiteral Object  { get; init; }

    [SetsRequiredMembers]
    public AdditionalLingualProperty(string predicate, LingualLiteral obj)
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

public class LingualLiteral
{
    public required string Text { get; init; }

    public string? LanguageTag { get; init; }

    [SetsRequiredMembers]
    public LingualLiteral(string text, string? languageTag = null)
    {
        Text = text;
        LanguageTag = languageTag;
    }
}
