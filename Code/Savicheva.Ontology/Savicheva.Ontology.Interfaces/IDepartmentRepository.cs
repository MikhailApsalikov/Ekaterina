namespace Savicheva.Ontology.Interfaces
{
	using System.Collections.Generic;
	using Entities;

	public interface IDepartmentRepository
	{
		List<IdTitle> GetAll();
	}
}