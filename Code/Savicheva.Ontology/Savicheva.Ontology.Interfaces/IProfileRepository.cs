namespace Savicheva.Ontology.Interfaces
{
	using System.Collections.Generic;
	using Entities;

	public interface IProfileRepository
	{
		List<IdTitle> GetAll();
	}
}