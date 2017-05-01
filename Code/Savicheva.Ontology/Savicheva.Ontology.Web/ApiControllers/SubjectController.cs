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
		public IHttpActionResult Get([FromUri]SubjectFilter filter)
		{
			try
			{
				List<SubjectModel> list = _repository.GetAll(filter).Select(MapEntityToModel).OrderBy(s => s.Id).ToList();
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

		[HttpPost]
		public virtual IHttpActionResult Post([FromBody]SubjectModel.UpdateData value)
		{
			try
			{
				return Ok(new RepositoryModifyResult<Subject>(new Subject()
				{
					Id = _repository.Create(MapModelToEntity(value))
				}));
			}
			catch (Exception ex)
			{
				return this.HandleException(ex);
			}
		}

		[HttpPut]
		public virtual IHttpActionResult Put(int id, [FromBody]SubjectModel.UpdateData value)
		{
			try
			{
				_repository.Update(id, MapModelToEntity(value));
				return Ok();
			}
			catch (Exception ex)
			{
				return this.HandleException(ex);
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

		private Subject MapModelToEntity(SubjectModel.UpdateData model)
		{
			return Mapper.Map<Subject>(model);
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