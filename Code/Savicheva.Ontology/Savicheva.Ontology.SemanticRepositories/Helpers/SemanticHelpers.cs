namespace Savicheva.Ontology.SemanticRepositories.Helpers
{
	using System;
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

		public static int GetId(this OntologyResource resource)
		{
			var node = resource.Resource as UriNode;
			// ReSharper disable once PossibleNullReferenceException
			return int.Parse(node.Uri.Segments.Last());
		}
	}
}