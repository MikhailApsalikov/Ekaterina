namespace Savicheva.Ontology.SemanticRepositories
{
	using System.Collections.Generic;
	using Entities;
	using Interfaces;
	using Selp.Common.Entities;
	using System.Linq;
	using Helpers;
	using VDS.RDF.Ontology;

	public class SubjectRepository : SemanticRepositoryBase, ISubjectRepository
	{
		public List<Subject> GetAll()
		{
			OntologyClass ontClass = GetClass("Subject");
			return ontClass.Instances.Select(Map).ToList();
		}

		public Subject GetById(int id)
		{
			return null;
		}

		public void Remove(int id)
		{
			
		}

		public RepositoryModifyResult<Subject> Create(Subject entity)
		{
			return new RepositoryModifyResult<Subject>(entity);
		}

		public RepositoryModifyResult<Subject> Update(int id, Subject entity)
		{
			return new RepositoryModifyResult<Subject>(entity);
		}

		private Subject Map(OntologyResource instance)
		{
			var result = new Subject()
			{
				HasHourForInd = instance.GetIntProperty("hasHourForInd"),
				HasHourForKoll = instance.GetIntProperty("hasHourForKoll"),
				HasHourForLab = instance.GetIntProperty("hasHourForLab"),
				HasHourForLecture = instance.GetIntProperty("hasHourForLecture"),
				HasHourForPract = instance.GetIntProperty("hasHourForPract"),
				Title = instance.GetStringProperty("title"),
				Id = instance.GetId(),
			};
			return result;
		}
	}
}