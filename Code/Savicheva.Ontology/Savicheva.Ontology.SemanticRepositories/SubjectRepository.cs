namespace Savicheva.Ontology.SemanticRepositories
{
	using System.Collections.Generic;
	using System.Linq;
	using Entities;
	using Helpers;
	using Interfaces;
	using Selp.Common.Entities;
	using VDS.RDF;
	using VDS.RDF.Ontology;

	public class SubjectRepository : SemanticRepositoryBase<Subject>, ISubjectRepository
	{
		private readonly IFormsOfControlRepository _formsOfControlRepository;

		public SubjectRepository(IGraphProxy graphProxy,
			IFormsOfControlRepository formsOfControlRepository) : base(graphProxy)
		{
			_formsOfControlRepository = formsOfControlRepository;
		}


		protected override string EntityName => "Subject";

		public RepositoryModifyResult<Subject> Create(Subject entity)
		{
			return new RepositoryModifyResult<Subject>(entity);
		}

		public RepositoryModifyResult<Subject> Update(int id, Subject entity)
		{
			return new RepositoryModifyResult<Subject>(entity);
		}

		public List<Subject> GetAll(SubjectFilter filter)
		{
			IEnumerable<Subject> result = base.GetAll();
			if (filter == null)
			{
				return result.ToList();
			}

			if (!string.IsNullOrEmpty(filter.Title))
			{
				result = result.Where(s => s.Title.Contains(filter.Title));
			}

			if (filter.FormsOfControl.HasValue)
			{
				result = result.Where(s => s.FormsOfControl.Any(f=>f.Id == filter.FormsOfControl.Value));
			}

			if (filter.HasHourForInd.HasValue)
			{
				result = result.Where(s => s.HasHourForInd == filter.HasHourForInd.Value);
			}

			if (filter.HasHourForLecture.HasValue)
			{
				result = result.Where(s => s.HasHourForLecture == filter.HasHourForLecture.Value);
			}

			if (filter.HasHourForKoll.HasValue)
			{
				result = result.Where(s => s.HasHourForKoll == filter.HasHourForKoll.Value);
			}

			if (filter.HasHourForLecture.HasValue)
			{
				result = result.Where(s => s.HasHourForLecture == filter.HasHourForLecture.Value);
			}

			if (filter.HasHourForLab.HasValue)
			{
				result = result.Where(s => s.HasHourForLab == filter.HasHourForLab.Value);
			}

			if (filter.HasHourForPract.HasValue)
			{
				result = result.Where(s => s.HasHourForPract == filter.HasHourForPract.Value);
			}

			return result.ToList();
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
				FormsOfControl = MapFormOfControl(instance)
			};
			return result;
		}

		private List<FormOfControl> MapFormOfControl(OntologyResource instance)
		{
			List<UriNode> formsOfControl = instance.GetObjectProperties("hasForm", GraphProxy.Graph);

			return formsOfControl.Select(f => _formsOfControlRepository.GetById(f.GetId())).ToList();
		}
	}
}