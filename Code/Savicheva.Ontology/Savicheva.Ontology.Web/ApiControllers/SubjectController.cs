namespace Savicheva.Ontology.Web.ApiControllers
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Http;
	using AutoMapper;
	using Entities;
	using Interfaces;
	using Models;
	using Selp.Common.Entities;

	public class SubjectController : ApiController
	{
		private readonly ISubjectRepository _repository;

		public SubjectController(ISubjectRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public IHttpActionResult Get(int id)
		{
			return Ok(MapEntityToModel(_repository.GetById(id)));
		}

		[HttpGet]
		public IHttpActionResult Get()
		{
			List<SubjectModel> list = _repository.GetAll().Select(MapEntityToShortModel).OrderBy(s=>s.Id).ToList();
			var content = new EntitiesListResult<SubjectModel>
			{
				Data = list,
				Page = -1,
				PageSize = -1
			};
			;
			content.Total = list.Count;
			return Ok(content);
		}


		private SubjectModel MapEntityToModel(Subject entity)
		{
			return Mapper.Map<SubjectModel>(entity);
		}

		private Subject MapModelToEntity(SubjectModel model)
		{
			return Mapper.Map<Subject>(model);
		}

		private SubjectModel MapEntityToShortModel(Subject entity)
		{
			return Mapper.Map<SubjectModel>(entity);
		}
	}
}