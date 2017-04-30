namespace Savicheva.Ontology.SemanticRepositories
{
	using System.Web.Hosting;
	using Interfaces;
	using VDS.RDF;
	using VDS.RDF.Ontology;

	public class GraphProxy : IGraphProxy
	{
		public const string OntologyPath = "~/App_Data/Ontology.owl";

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
	}
}