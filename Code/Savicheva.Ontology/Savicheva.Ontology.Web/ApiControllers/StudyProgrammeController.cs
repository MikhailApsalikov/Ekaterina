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

	public class StudyProgrammeController : ApiController
	{
		private readonly IStudyProgrammeRepository repository;

		public StudyProgrammeController(IStudyProgrammeRepository repository)
		{
			this.repository = repository;
		}

		[HttpGet]
		public IHttpActionResult Get(string id)
		{
			try
			{
				return Ok(MapEntityToModel(repository.GetById(id)));
			}
			catch (Exception e)
			{
				return HandleException(e);
			}
		}

		[HttpGet]
		public IHttpActionResult Get([FromUri] StudyProgrammeFilter filter)
		{
			try
			{
				List<StudyProgrammeModel> list = repository.GetAll(filter).Select(MapEntityToModel).OrderBy(s => s.Id).ToList();
				var content = new EntitiesListResult<StudyProgrammeModel>
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
		public virtual IHttpActionResult Post([FromBody] StudyProgrammeModel.UpdateData value)
		{
			try
			{
				return Ok(new RepositoryModifyResult<StudyProgramme>(new StudyProgramme
				{
					Id = repository.Create(MapModelToEntity(value))
				}));
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpPut]
		public virtual IHttpActionResult Put(string id, [FromBody] StudyProgrammeModel.UpdateData value)
		{
			try
			{
				repository.Update(id, MapModelToEntity(value));
				return Ok();
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpDelete]
		public virtual IHttpActionResult Delete(string id)
		{
			try
			{
				repository.Remove(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		private StudyProgrammeModel MapEntityToModel(StudyProgramme entity)
		{
			return Mapper.Map<StudyProgrammeModel>(entity);
		}

		private StudyProgramme MapModelToEntity(StudyProgrammeModel.UpdateData model)
		{
			return Mapper.Map<StudyProgramme>(model);
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