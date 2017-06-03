namespace Savicheva.Ontology.Interfaces
{
	using System.Collections.Generic;
	using Entities;

	public interface IDirectionRepository
	{
		List<IdTitle> GetAll();
	}
}