namespace Savicheva.Ontology.Interfaces
{
	using System.Collections.Generic;
	using Entities;
	using VDS.RDF;

	public interface IFormsOfControlRepository
	{
		FormOfControl GetById(int id);
		IEnumerable<UriNode> GetAllNodes();
	}
}