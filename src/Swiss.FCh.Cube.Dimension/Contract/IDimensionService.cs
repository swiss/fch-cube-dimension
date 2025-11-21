using System.Collections.Generic;
using Swiss.FCh.Cube.Dimension.Model;
using VDS.RDF;

namespace Swiss.FCh.Cube.Dimension.Contract
{
    public interface IDimensionService
    {
        /// <summary>
        /// This method converts the provided input into triples forming a dimension according to https://cube.link/.
        /// Writing these triples to a remote sparql endpoint is the responsibility of the application using this library.
        /// </summary>
        /// <param name="items">The items that should be added to the dimension (e.g. People, Departments, etc.).</param>
        /// <param name="graph">RDF graph used to create the triples. The graph's 'BaseUrl' must be specified./></param>
        /// <param name="dimensionUri">The URI of the dimension. Should equal the graphs base URI followed by the name of the dimension.</param>
        /// <param name="dimensionName">Multi language name / description of the dimension</param>
        /// <param name="additionalRdfNamespaces">List of RDF additional namespaces to add to the graph ("schema" and "rdf" are added by the library).</param>
        /// <param name="rdfTypes">RDF types (e.g. http://schema.org/Person)</param>
        /// <returns>All the triples forming the dimension</returns>
        IEnumerable<Triple> CreateTriples(
            IEnumerable<DimensionItem> items,
            Graph graph,
            string dimensionUri,
            IList<Literal> dimensionName = null,
            IList<RdfNamespace> additionalRdfNamespaces = null,
            IList<string> rdfTypes = null);
    }
}

