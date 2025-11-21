
namespace Swiss.FCh.Cube.Dimension.Model
{
    public class RdfNamespace
    {
        public string Prefix { get; set; }

        public string Uri { get; set; }

        public static readonly RdfNamespace Schema = new RdfNamespace("schema", "http://schema.org");
        public static readonly RdfNamespace Rdf = new RdfNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");

        // ReSharper disable once MemberCanBePrivate.Global : is used by the consumers of the library
        public RdfNamespace(string prefix, string uri)
        {
            Prefix = prefix;
            Uri = uri;
        }
    }
}
