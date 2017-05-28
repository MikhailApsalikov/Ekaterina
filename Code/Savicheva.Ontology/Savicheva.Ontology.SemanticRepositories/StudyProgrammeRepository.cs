namespace Savicheva.Ontology.SemanticRepositories
{
	using System.Collections.Generic;
	using System.Linq;
	using Entities;
	using Helpers;
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
			return new StudyProgramme
			{
				Title = instance.GetStringProperty("title"),
				Id = instance.GetId(),
				Direction = MapIdTitle(instance.GetObjectProperties("hasNapr").FirstOrDefault()?.GetId()),
				Department = MapIdTitle(instance.GetObjectProperties("hasDepartment").FirstOrDefault()?.GetId()),
				Profile = MapIdTitle(instance.GetObjectProperties("hasProfile").FirstOrDefault()?.GetId()),
				Subjects = instance.GetObjectProperties("hasSubject").Select(s=>subjectRepository.GetShortById(s.GetId())).ToList()
			};
		}

		protected override void SetProperties(StudyProgramme entity, Individual instance)
		{
		}

		public List<StudyProgramme> GetAll(StudyProgrammeFilter filter)
		{
			IEnumerable<StudyProgramme> result = base.GetAll();
			if (filter == null)
			{
				return result.ToList();
			}

			if (!string.IsNullOrEmpty(filter.Title))
			{
				result = result.Where(s => s.Title.Contains(filter.Title));
			}
			if (!string.IsNullOrEmpty(filter.Department))
			{
				result = result.Where(s => s.Department?.Title.Contains(filter.Department) ?? false);
			}
			if (!string.IsNullOrEmpty(filter.Direction))
			{
				result = result.Where(s => s.Direction?.Title.Contains(filter.Direction) ?? false);
			}
			return result.ToList();
		}
	}
}