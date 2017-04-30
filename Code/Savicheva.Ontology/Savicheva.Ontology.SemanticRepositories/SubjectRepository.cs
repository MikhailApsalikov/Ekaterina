namespace Savicheva.Ontology.SemanticRepositories
{
	using System.Collections.Generic;
	using Entities;
	using Interfaces;
	using Selp.Common.Entities;
	using System.Linq;
	using Helpers;
	using VDS.RDF.Ontology;

	public class SubjectRepository : SemanticRepositoryBase<Subject>, ISubjectRepository
	{
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

		protected override Subject Map(OntologyResource instance)
		{
			var result = new Subject
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

		protected override string EntityName => "Subject";
	}
}