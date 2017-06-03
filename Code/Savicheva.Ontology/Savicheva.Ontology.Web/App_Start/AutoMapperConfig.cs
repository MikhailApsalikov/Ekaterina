namespace Savicheva.Ontology.Web
{
	using System.Linq;
	using AutoMapper;
	using Entities;
	using Models;

	public static class AutoMapperConfig
	{
		public static void RegisterMappings()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<Subject, SubjectModel>();
				cfg.CreateMap<SubjectModel.UpdateData, Subject>()
					.ForMember(s => s.FormsOfControl, s => s.MapFrom(c => c.FormsOfControl.Select(d => new IdTitle { Id = d})))
					.ForMember(s => s.Modules, s => s.MapFrom(c => c.Modules.Select(d => new IdTitle { Id = d })));
				cfg.CreateMap<StudyProgramme, StudyProgrammeModel>();
				cfg.CreateMap<StudyProgramme, IdTitle>();
				cfg.CreateMap<StudyProgrammeModel.UpdateData, StudyProgramme>()
					.ForMember(s => s.Subjects, s => s.MapFrom(c => c.Subjects.Select(d => new Subject { Id = d })))
					.ForMember(s => s.Department, s => s.MapFrom(d => new IdTitle { Id = d.Department }))
					.ForMember(s => s.Direction, s => s.MapFrom(d => new IdTitle { Id = d.Direction }))
					.ForMember(s => s.Profile, s => s.MapFrom(d => new IdTitle { Id = d.Profile }));
				cfg.CreateMap<AccountModel, Account>().ReverseMap();
			});
		}
	}
}