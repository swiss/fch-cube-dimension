using System;
using System.Collections.Generic;
using System.Linq;

namespace Swiss.FCh.Cube.Dimension.Model
{
    public class DimensionItem
    {
        public object Key { get; set; }

        public Literal Name { get; set; }

        public List<AdditionalLiteralProperty> AdditionalLiteralProperties { get; } = new List<AdditionalLiteralProperty>();

        public List<AdditionalUriProperty> AdditionalUriProperties { get; } = new List<AdditionalUriProperty>();

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

    public class AdditionalLiteralProperty
    {
        public string Predicate { get; set; }

        public Literal Object { get; set; }

        public AdditionalLiteralProperty(string predicate, Literal obj)
        {
            Predicate = predicate;
            Object = obj;
        }
    }

    public class AdditionalUriProperty
    {
        public string Predicate { get; set; }

        public string Object { get; set; }

        public AdditionalUriProperty(string predicate, string obj)
        {
            Predicate = predicate;
            Object = obj;
        }
    }

    public class Literal
    {
        public string Text { get; set; }

        public string LanguageTag { get; set; }

        public Uri DataType { get; set; }

        public Literal(string text, string languageTag = null)
        {
            Text = text;
            LanguageTag = languageTag;
        }

        public Literal(string text, Uri dataType)
        {
            Text = text;
            DataType = dataType;
        }
    }
}
