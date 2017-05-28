namespace Savicheva.Ontology.SemanticRepositories
{
	using System.Collections.Generic;
	using Entities;
	using Interfaces;
	using VDS.RDF.Ontology;

	public class StudyProgrammeRepository : SemanticRepositoryBase<StudyProgramme>, IStudyProgrammeRepository
	{
		private readonly ISubjectRepository subjectRepository;

		public StudyProgrammeRepository(IGraphProxy graphProxy, ISubjectRepository subjectRepository) : base(graphProxy)
		{
			this.subjectRepository = subjectRepository;
		}

		protected override string EntityName => "StudyProgramme";
		protected override StudyProgramme Map(OntologyResource instance)
		{
			return new StudyProgramme();
		}

		protected override void SetProperties(StudyProgramme entity, Individual instance)
		{
		}

		public List<StudyProgramme> GetAll(StudyProgrammeFilter filter)
		{
			var result = base.GetAll();
			return result;
		}
	}
}