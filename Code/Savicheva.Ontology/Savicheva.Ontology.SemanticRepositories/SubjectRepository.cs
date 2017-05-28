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

	public class SubjectRepository : SemanticRepositoryBase<Subject>, ISubjectRepository
	{
		private readonly IFormsOfControlRepository formsOfControlRepository;
		private readonly IModuleRepository moduleRepository;

		public SubjectRepository(IGraphProxy graphProxy,
			IFormsOfControlRepository formsOfControlRepository, IModuleRepository moduleRepository) : base(graphProxy)
		{
			this.formsOfControlRepository = formsOfControlRepository;
			this.moduleRepository = moduleRepository;
		}

		protected override string EntityName => "Subject";

		public List<Subject> GetAll(SubjectFilter filter)
		{
			IEnumerable<Subject> result = base.GetAll();
			if (filter == null)
			{
				return result.ToList();
			}

			if (!string.IsNullOrEmpty(filter.Title))
			{
				result = result.Where(s => s.Title.ToUpperInvariant().Contains(filter.Title.ToUpperInvariant()));
			}

			if (!string.IsNullOrEmpty(filter.FormsOfControl))
			{
				result = result.Where(s => s.FormsOfControl.Any(f => f.Id == filter.FormsOfControl));
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

		public Subject GetShortById(string id)
		{
			OntologyResource instance = GraphProxy.Graph.CreateOntologyResource(new Uri(id));
			return Map(instance, false);
		}

		protected override Subject Map(OntologyResource instance)
		{
			return Map(instance, true);
		}

		private Subject Map(OntologyResource instance, bool isStudyProgrammeRequired)
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
				FormsOfControl = MapFormOfControl(instance),
				Modules = MapModules(instance)
			};
			if (isStudyProgrammeRequired)
			{
				IdTitle studyProgramme = MapIdTitle(instance.GetSubjectsByObjectProperty("hasSubject").FirstOrDefault()?.GetId());
				if (studyProgramme != null)
				{
					result.StudyProgramme = new StudyProgramme()
					{
						Id = studyProgramme.Id,
						Title = studyProgramme.Title
					};
				}
			}

			return result;
		}

		protected override void SetProperties(Subject entity, Individual instance)
		{
			instance.SetStringProperty("title", entity.Title);
			instance.SetIntProperty("hasHourForInd", entity.HasHourForInd);
			instance.SetIntProperty("hasHourForKoll", entity.HasHourForKoll);
			instance.SetIntProperty("hasHourForLab", entity.HasHourForLab);
			instance.SetIntProperty("hasHourForLecture", entity.HasHourForLecture);
			instance.SetIntProperty("hasHourForPract", entity.HasHourForPract);
			instance.SetObjectProperties("hasForm", FormOfControlToUriNodes(entity.FormsOfControl));
			instance.SetSubjectsByObjectProperty("includesSubject", ModulesToUriNodes(entity.Modules));
		}

		private List<UriNode> ModulesToUriNodes(List<IdTitle> modules)
		{
			Dictionary<string, UriNode> moduleNodes = moduleRepository.GetAllNodes().ToDictionary(s => s.GetId());
			return modules.Select(s => moduleNodes[s.Id]).ToList();
		}

		private List<UriNode> FormOfControlToUriNodes(List<IdTitle> focs)
		{
			Dictionary<string, UriNode> formsOfControlNodes = formsOfControlRepository.GetAllNodes()
				.ToDictionary(s => s.GetId());
			return focs.Select(s => formsOfControlNodes[s.Id]).ToList();
		}

		private List<IdTitle> MapFormOfControl(OntologyResource instance)
		{
			List<UriNode> formsOfControl = instance.GetObjectProperties("hasForm");

			return formsOfControl.Select(f => formsOfControlRepository.GetById(f.GetId())).ToList();
		}

		private List<IdTitle> MapModules(OntologyResource instance)
		{
			List<OntologyResource> result = instance.GetSubjectsByObjectProperty("includesSubject");
			return result.Select(f => moduleRepository.GetById(f.GetId())).ToList();
		}
	}
}