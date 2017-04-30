namespace Savicheva.Ontology.SemanticRepositories.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using VDS.RDF;
	using VDS.RDF.Ontology;

	internal static class SemanticHelpers
	{
		public static int? GetIntProperty(this OntologyResource resource, string propertyName)
		{
			Triple triple = resource.TriplesWithSubject.FirstOrDefault(s => s.Predicate.ToString().EndsWith(propertyName));
			LiteralNode ln = triple?.Object as LiteralNode;
			if (ln == null)
			{
				return null;
			}
			try
			{
				return (int?)(double.Parse(ln.Value));
			}
			catch (FormatException)
			{
				return null;
			}		
		}

		public static string GetStringProperty(this OntologyResource resource, string propertyName)
		{
			Triple triple = resource.TriplesWithSubject.FirstOrDefault(s => s.Predicate.ToString().EndsWith(propertyName));
			LiteralNode ln = triple?.Object as LiteralNode;
			return ln?.Value;
		}

		public static List<UriNode> GetObjectProperties(this OntologyResource resource, string propertyName, IGraph graph)
		{
			List<Triple> triples = resource.TriplesWithSubject.Where(s => s.Predicate.ToString().EndsWith(propertyName)).ToList();
			return triples.Select(t => t.Object).Cast<UriNode>().ToList();
		}

		public static int GetId(this OntologyResource resource)
		{
			var node = resource.Resource as UriNode;
			return GetId(node);
		}

		public static int GetId(this UriNode uriNode)
		{
			// ReSharper disable once PossibleNullReferenceException
			return int.Parse(uriNode.Uri.Segments.Last());
		}
	}
}