namespace Savicheva.Ontology.SemanticRepositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Entities;
	using Helpers;
	using Interfaces;
	using VDS.RDF;
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
			instance.SetStringProperty("title", entity.Title);
			if (!string.IsNullOrEmpty(entity.Department.Id))
			{
				instance.SetObjectProperties("hasDepartment", new List<IUriNode>
					{
						GraphProxy.Graph.GetUriNode(new Uri(entity.Department.Id))
					}.Cast<UriNode>()
					.ToList());
			}
			else
			{
				instance.SetObjectProperties("hasDepartment", null);
			}
			if (!string.IsNullOrEmpty(entity.Profile.Id))
			{
				instance.SetObjectProperties("hasProfile", new List<IUriNode>
					{
						GraphProxy.Graph.GetUriNode(new Uri(entity.Profile.Id))
					}.Cast<UriNode>()
					.ToList());
			}
			else
			{
				instance.SetObjectProperties("hasProfile", null);
			}
			if (!string.IsNullOrEmpty(entity.Direction.Id))
			{
				instance.SetObjectProperties("hasNapr", new List<IUriNode>
					{
						GraphProxy.Graph.GetUriNode(new Uri(entity.Direction.Id))
					}.Cast<UriNode>()
					.ToList());
			}
			else
			{
				instance.SetObjectProperties("hasNapr", null);
			}
			instance.SetObjectProperties("hasSubject", entity.Subjects.Select(s=> GraphProxy.Graph.GetUriNode(new Uri(s.Id))).Cast<UriNode>().ToList());
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
				result = result.Where(s => s.Title.ToUpperInvariant().Contains(filter.Title.ToUpperInvariant()));
			}
			if (!string.IsNullOrEmpty(filter.Department))
			{
				result = result.Where(s => s.Department?.Title?.ToUpperInvariant().Contains(filter.Department.ToUpperInvariant()) ?? false);
			}
			if (!string.IsNullOrEmpty(filter.Direction))
			{
				result = result.Where(s => s.Direction?.Title?.ToUpperInvariant().Contains(filter.Direction.ToUpperInvariant()) ?? false);
			}
			return result.ToList();
		}
	}
}