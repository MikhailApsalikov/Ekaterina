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
				cfg.CreateMap<AccountModel, Account>().ReverseMap();
			});
		}
	}
}