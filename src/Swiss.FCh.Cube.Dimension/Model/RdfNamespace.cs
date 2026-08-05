
namespace Swiss.FCh.Cube.Dimension.Model
{
    /// <summary>
    /// This class represents an RDF namespace that can be passed to the <see cref="Swiss.FCh.Cube.Dimension.Contract.IDimensionService"/> in order register a namespace for the dimension.
    /// </summary>
    public class RdfNamespace
    {
        /// <summary>
        /// Prefix of the namespace (used in your RDF URIs to use this namespace).
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Full URI of the namespace.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// schema.org URI
        /// </summary>
        public static readonly RdfNamespace Schema = new RdfNamespace("schema", "http://schema.org");

        /// <summary>
        /// RDF syntax URI
        /// </summary>
        public static readonly RdfNamespace Rdf = new RdfNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");

        // ReSharper disable once MemberCanBePrivate.Global : is used by the consumers of the library
        /// <summary>
        /// Constructor to create a new RDF namespace.
        /// </summary>
        /// <param name="prefix">Prefix of the namespace (used in your RDF URIs to use this namespace).</param>
        /// <param name="uri">Full URI of the namespace.</param>
        public RdfNamespace(string prefix, string uri)
        {
            Prefix = prefix;
            Uri = uri;
        }
    }
}
