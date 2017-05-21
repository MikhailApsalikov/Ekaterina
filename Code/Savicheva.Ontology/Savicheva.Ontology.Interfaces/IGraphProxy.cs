namespace Savicheva.Ontology.Interfaces
{
	using System.IO;
	using VDS.RDF.Ontology;

	public interface IGraphProxy
	{
		OntologyGraph Graph { get; }
		void LoadGraph();
		void SaveChanges();
		int AddTripplesFromStream(Stream stream);
		string GenerateId();
	}
}