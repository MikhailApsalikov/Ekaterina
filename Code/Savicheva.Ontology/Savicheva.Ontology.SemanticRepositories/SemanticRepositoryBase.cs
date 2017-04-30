namespace Savicheva.Ontology.SemanticRepositories
{
	using System.IO;
	using System.Linq;
	using System.Web.Hosting;
	using VDS.RDF;
	using VDS.RDF.Ontology;

	public abstract class SemanticRepositoryBase
	{
		public OntologyGraph Graph { get; private set; }

		public string Filename { get; }

		protected SemanticRepositoryBase()
		{
			Filename = Path.Combine(HostingEnvironment.MapPath("~/App_Data/Ontology.owl"));
			LoadGraph();
		}

		protected void LoadGraph()
		{
			Graph = new OntologyGraph();
			Graph.LoadFromFile(Filename);
		}

		protected OntologyClass GetClass(string name)
		{
			return Graph.OwlClasses.FirstOrDefault(c=>c.Resource.ToString().Contains(name));
		} 
	}
}