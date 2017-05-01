namespace Savicheva.Ontology.Interfaces
{
	using System.Collections.Generic;
	using Entities;

	public interface ISubjectRepository
	{
		List<Subject> GetAll(SubjectFilter filter);
		Subject GetById(int id);
		void Remove(int id);
		int Create(Subject entity);
		void Update(int id, Subject entity);
	}
}