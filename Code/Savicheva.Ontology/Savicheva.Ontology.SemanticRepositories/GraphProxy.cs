namespace Savicheva.Ontology.SemanticRepositories
{
	using System.Linq;
	using System.Web.Hosting;
	using Helpers;
	using Interfaces;
	using VDS.RDF;
	using VDS.RDF.Ontology;

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

		public void SaveChanges()
		{
			lock (_lock)
			{
				Graph.SaveToFile(HostingEnvironment.MapPath(OntologyPath));
			}
		}

		public int GenerateId()
		{
			if (id.HasValue)
			{
				id = id + 1;
				return id.Value;
			}

			id = Graph.Nodes.Where(n => n is UriNode && n.ToString().Contains(IndividualsDomain))
				.Cast<UriNode>()
				.Select(n => n.GetId())
				.Max() + 1;
			return id ?? 1;

		}
	}
}