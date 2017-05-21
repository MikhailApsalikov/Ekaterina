namespace Savicheva.Ontology.Web.ApiControllers
{
	using System;
	using System.Web;
	using System.Web.Http;
	using Interfaces;

	public class RdfController : ApiController
	{
		private readonly IGraphProxy graphProxy;

		public RdfController(IGraphProxy graphProxy)
		{
			this.graphProxy = graphProxy;
		}

		[HttpPost]
		public IHttpActionResult Import()
		{
			try
			{
				if (HttpContext.Current.Request.Files.AllKeys.Length < 1)
				{
					throw new ArgumentException("Не указан файл!");
				}

				return Ok(graphProxy.AddTripplesFromStream(HttpContext.Current.Request.Files[0].InputStream));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}