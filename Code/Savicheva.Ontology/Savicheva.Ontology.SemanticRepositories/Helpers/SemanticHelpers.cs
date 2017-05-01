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
			LiteralNode ln = resource.GetProperty(propertyName) as LiteralNode;
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
			return (resource.GetProperty(propertyName) as LiteralNode)?.Value;
		}

		public static List<UriNode> GetObjectProperties(this OntologyResource resource, string propertyName)
		{
			return resource.GetProperties(propertyName).Cast<UriNode>().ToList();
		}

		public static void SetIntProperty(this OntologyResource resource, string propertyName, int? value)
		{
			resource.SetProperty(propertyName, value.HasValue ? resource.Graph.CreateLiteralNode(value.ToString()) : null);
		}

		public static void SetStringProperty(this OntologyResource resource, string propertyName, string value)
		{
			resource.SetProperty(propertyName, value != null ? resource.Graph.CreateLiteralNode(value) : null);
		}

		public static void SetObjectProperties(this OntologyResource resource, string propertyName, List<UriNode> nodes)
		{
			resource.SetProperties(propertyName, nodes?.Cast<INode>().ToList() ?? new List<INode>());
		}

		// Common
		public static INode GetProperty(this OntologyResource resource, string propertyName)
		{
			Triple triple = resource.TriplesWithSubject.FirstOrDefault(s => s.Predicate.ToString().EndsWith(propertyName));
			return triple?.Object;
		}

		public static List<INode> GetProperties(this OntologyResource resource, string propertyName)
		{
			List<Triple> triples = resource.TriplesWithSubject.Where(s => s.Predicate.ToString().EndsWith(propertyName)).ToList();
			return triples.Select(t => t.Object).ToList();
		}

		public static void SetProperty(this OntologyResource resource, string propertyName, INode value)
		{
			resource.RemoveProperty(propertyName);
			OntologyProperty property = ((OntologyGraph)resource.Graph).OwlProperties.FirstOrDefault(s => s.Resource.ToString().EndsWith(propertyName));
			if (value != null && property != null)
			{
				Triple triple = new Triple(resource.Resource, property.Resource, value, resource.Graph);
				resource.Graph.Assert(triple);
			}
		}

		public static void SetProperties(this OntologyResource resource, string propertyName, List<INode> values)
		{
			resource.RemoveProperty(propertyName);
			OntologyProperty property = ((OntologyGraph)resource.Graph).OwlProperties.FirstOrDefault(s => s.Resource.ToString().EndsWith(propertyName));
			if (values != null && values.Any() && property != null)
			{
				IEnumerable<Triple> triples = values.Select(v => new Triple(resource.Resource, property.Resource, v, resource.Graph));
				resource.Graph.Assert(triples);
			}
		}

		public static void RemoveProperty(this OntologyResource resource, string propertyName)
		{
			resource.Graph.Retract(resource.TriplesWithSubject.Where(s => s.Predicate.ToString().EndsWith(propertyName)).ToList());
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