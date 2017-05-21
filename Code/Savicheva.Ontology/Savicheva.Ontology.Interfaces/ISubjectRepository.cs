namespace Savicheva.Ontology.Interfaces
{
	using System.Collections.Generic;
	using Entities;

	public interface ISubjectRepository
	{
		List<Subject> GetAll(SubjectFilter filter);
		Subject GetById(string id);
		void Remove(string id);
		string Create(Subject entity);
		void Update(string id, Subject entity);
	}
}