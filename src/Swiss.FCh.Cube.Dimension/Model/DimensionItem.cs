using System;
using System.Collections.Generic;
using System.Linq;

namespace Swiss.FCh.Cube.Dimension.Model
{
    /// <summary>
    /// Use this class to describe your "dimension" according to https://cube.link.
    /// Pass these objects to the <see cref="Swiss.FCh.Cube.Dimension.Contract.IDimensionService"/> to create the tripels for the dimension.
    /// </summary>
    public class DimensionItem
    {
        /// <summary>
        /// Unique identifier for the dimension item (usually a string but it can be of any type).
        /// </summary>
        public object Key { get; set; }

        /// <summary>
        /// Name of the dimension item (multi lingual).
        /// </summary>
        public Literal Name { get; set; }

        /// <summary>
        /// Additional 'literal' properties of the dimension item can be added here.
        /// </summary>
        public List<AdditionalLiteralProperty> AdditionalLiteralProperties { get; } = new List<AdditionalLiteralProperty>();

        /// <summary>
        /// Use these links, if the dimension item needs to reference antoher concept via an URI.
        /// </summary>
        public List<AdditionalUriProperty> AdditionalUriProperties { get; } = new List<AdditionalUriProperty>();

        /// <summary>
        /// Constructor to create a new dimension item.
        /// </summary>
        /// <param name="key">Unique identifier of the dimension item</param>
        /// <param name="name">Name of the dimension item</param>
        /// <param name="additionalProperties">Additional 'literal' properties</param>
        /// <param name="additionalUriProperties">Additional properties referencing another concept via URI</param>
        public DimensionItem(
            object key,
            Literal name,
            IList<AdditionalLiteralProperty> additionalProperties = null,
            IList<AdditionalUriProperty> additionalUriProperties = null)
        {
            Key = key;
            Name = name;
            AdditionalLiteralProperties.AddRange(additionalProperties ?? Enumerable.Empty<AdditionalLiteralProperty>());
            AdditionalUriProperties.AddRange(additionalUriProperties ?? Enumerable.Empty<AdditionalUriProperty>());
        }
    }

    /// <summary>
    /// Representation of a 'literal' property consisting of a predicate and a multi lingual string object.
    /// </summary>
    public class AdditionalLiteralProperty
    {
        /// <summary>
        /// The RDF predicate representing this property.
        /// </summary>
        public string Predicate { get; set; }

        /// <summary>
        /// The RDF 'object' (value) of the property.
        /// </summary>
        public Literal Object { get; set; }

        /// <summary>
        /// Constructor to create a new additional literal property.
        /// </summary>
        /// <param name="predicate">RDF predicate of this property.</param>
        /// <param name="obj">RDF object (value) of this property.</param>
        public AdditionalLiteralProperty(string predicate, Literal obj)
        {
            Predicate = predicate;
            Object = obj;
        }
    }

    /// <summary>
    /// Representation of a property which is linked via an URI to another concept.
    /// </summary>
    public class AdditionalUriProperty
    {
        /// <summary>
        /// The RDF predicate representing this property.
        /// </summary>
        public string Predicate { get; set; }

        /// <summary>
        /// The RDF object (value) of this property in form of an URI (linking to another concept).
        /// </summary>
        public string Object { get; set; }

        /// <summary>
        /// Constructor to create a new additional URI property.
        /// </summary>
        /// <param name="predicate">The RDF predicate representing this property.</param>
        /// <param name="obj">The RDF object (value) of this property in form of an URI (linking to another concept).</param>
        public AdditionalUriProperty(string predicate, string obj)
        {
            Predicate = predicate;
            Object = obj;
        }
    }

    /// <summary>
    /// This class represents a multi lingual value.
    /// </summary>
    public class Literal
    {
        /// <summary>
        /// The actual text content of the property.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The RDF language tag specyfing the language of the text.
        /// </summary>
        public string LanguageTag { get; set; }

        /// <summary>
        /// If the value has a specific data type, use this property to specify it.
        /// This can only be used without language tag (see constructor overloads).
        /// </summary>
        public Uri DataType { get; set; }

        /// <summary>
        /// Constructor to create multi language literal.
        /// </summary>
        /// <param name="text">The text content.</param>
        /// <param name="languageTag">The RDF language tag.</param>
        public Literal(string text, string languageTag = null)
        {
            Text = text;
            LanguageTag = languageTag;
        }

        /// <summary>
        /// Constructor to create a value of a specific RDF data type.
        /// </summary>
        /// <param name="text">The value</param>
        /// <param name="dataType">RDF data type URI</param>
        public Literal(string text, Uri dataType)
        {
            Text = text;
            DataType = dataType;
        }
    }
}
