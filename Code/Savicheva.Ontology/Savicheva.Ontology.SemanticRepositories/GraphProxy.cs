namespace Savicheva.Ontology.SemanticRepositories
{
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Web.Hosting;
	using Helpers;
	using Interfaces;
	using VDS.RDF;
	using VDS.RDF.Ontology;
	using VDS.RDF.Parsing;

	public class GraphProxy : IGraphProxy
	{
		public const string OntologyPath = "~/App_Data/Ontology.owl";
		public const string IndividualsDomain = "http://localhost:3030";
		private int? id;

		private readonly object _lock = new object();
		public OntologyGraph Graph { get; private set; }

		public void LoadGraph()
		{
			if (Graph == null)
			{
				Graph = new OntologyGraph();
				Graph.LoadFromFile(HostingEnvironment.MapPath(OntologyPath));
			}
		}

		public int AddTripplesFromStream(Stream stream)
		{
			using (var reader = new StreamReader(stream, Encoding.UTF8))
			{
				IGraph graph = new Graph();
				graph.LoadFromString(reader.ReadToEnd(), new RdfXmlParser());
				Graph.Assert(graph.Triples);
				SaveChanges();
				return graph.Triples.Count;
			}
		}

		public void SaveChanges()
		{
			lock (_lock)
			{
				Graph.SaveToFile(HostingEnvironment.MapPath(OntologyPath));
			}
		}

		public string GenerateId()
		{
			int result;
			if (id.HasValue)
			{
				result = id.Value + 1;
			}
			else
			{
				result = Graph.Nodes.Where(n => n is UriNode && n.ToString().Contains(IndividualsDomain))
					     .Cast<UriNode>()
					     .Select(n => n.GetIntId())
					     .Max() + 1;
			}

			
			return IndividualsDomain + '/' + result;
		}
	}
}