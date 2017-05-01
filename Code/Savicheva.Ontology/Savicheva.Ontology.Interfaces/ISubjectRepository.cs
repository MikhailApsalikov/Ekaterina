namespace Savicheva.Ontology.Interfaces
{
	using System.Collections.Generic;
	using Entities;
	using Selp.Common.Entities;

	public interface ISubjectRepository
	{
		List<Subject> GetAll(SubjectFilter filter);
		Subject GetById(int id);
		void Remove(int id);
		RepositoryModifyResult<Subject> Create(Subject entity);
		RepositoryModifyResult<Subject> Update(int id, Subject entity);
	}
}