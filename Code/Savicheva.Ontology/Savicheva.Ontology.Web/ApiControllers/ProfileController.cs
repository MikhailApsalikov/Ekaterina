namespace Savicheva.Ontology.Web.ApiControllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
	using System.Web.Http;
	using Entities;
	using Interfaces;
	using Selp.Common.Entities;
	using Selp.Common.Exceptions;

	public class ProfileController : ApiController
	{
		private readonly IProfileRepository repository;

		public ProfileController(IProfileRepository repository)
		{
			this.repository = repository;
		}

		[HttpGet]
		public IHttpActionResult Get()
		{
			try
			{
				List<IdTitle> list = repository.GetAll().OrderBy(s => s.Id).ToList();
				var content = new EntitiesListResult<IdTitle>
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