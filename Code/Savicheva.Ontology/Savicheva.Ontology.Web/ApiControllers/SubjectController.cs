namespace Savicheva.Ontology.Web.ApiControllers
{
	using AutoMapper;
	using Entities;
	using Models;
	using Selp.Controller;
	using Selp.Interfaces;

	public class SubjectController : SelpController<SubjectModel, SubjectModel, Subject, int>
	{
		public SubjectController(ISelpRepository<Subject, int> repository) : base(repository)
		{
		}

		public override string ControllerName => "Subject";

		protected override SubjectModel MapEntityToModel(Subject entity)
		{
			return Mapper.Map<SubjectModel>(entity);
		}

		protected override Subject MapModelToEntity(SubjectModel model)
		{
			return Mapper.Map<Subject>(model);
		}

		protected override SubjectModel MapEntityToShortModel(Subject entity)
		{
			return Mapper.Map<SubjectModel>(entity);
		}
	}
}