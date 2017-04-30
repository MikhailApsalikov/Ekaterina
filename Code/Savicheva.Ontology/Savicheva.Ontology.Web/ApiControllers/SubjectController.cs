namespace Savicheva.Ontology.Web.ApiControllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
	using System.Web.Http;
	using AutoMapper;
	using Entities;
	using Interfaces;
	using Models;
	using Selp.Common.Entities;
	using Selp.Common.Exceptions;

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
			try
			{
				return Ok(MapEntityToModel(_repository.GetById(id)));
			}
			catch (Exception e)
			{
				return HandleException(e);
			}

		}

		[HttpGet]
		public IHttpActionResult Get()
		{
			try
			{
				List<SubjectModel> list = _repository.GetAll().Select(MapEntityToShortModel).OrderBy(s => s.Id).ToList();
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
			catch (Exception e)
			{
				return HandleException(e);
			}
		}

		[HttpDelete]
		public virtual IHttpActionResult Delete(int id)
		{
			try
			{
				_repository.Remove(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
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

		private IHttpActionResult HandleException(Exception e)
		{
			if (e is NotSupportedException)
			{
				return StatusCode(HttpStatusCode.MethodNotAllowed);
			}

			if (e is EntityNotFoundException)
			{
				return NotFound();
			}

			return InternalServerError(e);
		}
	}
}