namespace Savicheva.Ontology.Interfaces
{
	using VDS.RDF.Ontology;

	public interface IGraphProxy
	{
		OntologyGraph Graph { get; }
		void LoadGraph();
		void SaveChanges();
		int GenerateId();
	}
}