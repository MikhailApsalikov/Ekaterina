namespace Savicheva.Ontology.Web
{
	using AutoMapper;
	using Entities;
	using Models;

	public static class AutoMapperConfig
	{
		public static void RegisterMappings()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<SubjectModel, Subject>().ReverseMap();
				cfg.CreateMap<AccountModel, Account>().ReverseMap();
			});
		}
	}
}