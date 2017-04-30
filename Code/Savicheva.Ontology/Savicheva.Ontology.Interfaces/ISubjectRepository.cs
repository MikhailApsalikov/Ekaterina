namespace Savicheva.Ontology.Interfaces
{
	using System.Collections.Generic;
	using Entities;
	using Selp.Common.Entities;

	public interface ISubjectRepository
	{
		List<Subject> GetAll();
		Subject GetById(int id);
		void Remove(int id);
		RepositoryModifyResult<Subject> Create(Subject entity);
		List<Subject> GetByFilter(BaseFilter filter, out int total);
		RepositoryModifyResult<Subject> Update(int id, Subject entity);
	}
}