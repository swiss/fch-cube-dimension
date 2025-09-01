using System.Diagnostics.CodeAnalysis;

namespace Swiss.FCh.Dimension.Model;

public class RdfNamespace
{
    public required string Prefix { get; init; }

    public required string Uri { get; init; }

    public static readonly RdfNamespace Schema = new("schema", "http://schema.org");
    public static readonly RdfNamespace Rdf = new("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");

    [SetsRequiredMembers]
    // ReSharper disable once MemberCanBePrivate.Global : is used by the consumers of the library
    public RdfNamespace(string prefix, string uri)
    {
        Prefix = prefix;
        Uri = uri;
    }
}
