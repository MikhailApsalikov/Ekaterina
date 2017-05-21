namespace Savicheva.Ontology.Interfaces
{
	using System.Collections.Generic;
	using Entities;
	using VDS.RDF;

	public interface IModuleRepository
	{
		IdTitle GetById(string id);
		IEnumerable<UriNode> GetAllNodes();
	}
}