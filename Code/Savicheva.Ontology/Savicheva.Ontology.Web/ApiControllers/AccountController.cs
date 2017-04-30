namespace Savicheva.Ontology.Web.ApiControllers
{
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Web.Hosting;
	using System.Web.Http;
	using AutoMapper;
	using Entities;
	using Interfaces;
	using Models;
	using Selp.Common.Entities;
	using Selp.Controller;
	using SemanticRepositories;
	using Validators;

	public class AccountController : SelpController<AccountModel, AccountModel, Account, int>
	{
		public AccountController(IAccountRepository repository) : base(repository)
		{
		}

		public override string ControllerName => "Account";

		protected override AccountModel MapEntityToModel(Account entity)
		{
			return Mapper.Map<AccountModel>(entity);
		}

		protected override Account MapModelToEntity(AccountModel model)
		{
			return Mapper.Map<Account>(model);
		}

		protected override AccountModel MapEntityToShortModel(Account entity)
		{
			return Mapper.Map<AccountModel>(entity);
		}

		[Route("api/user/login")]
		[HttpPost]
		public IHttpActionResult Login([FromBody] AccountModel model)
		{
			var validator = new AccountLoginValidator(model);
			validator.Validate();
			if (!validator.IsValid)
				return Ok(new RepositoryModifyResult<AccountModel>(validator.Errors));

			List<Account> result =
				Repository.GetByCustomExpression(d => d.Name == model.Name);
			if (result.Count == 1)
			{
				AccountModel account = MapEntityToModel(result[0]);
				if (account.Password != model.Password)
					return Ok(new RepositoryModifyResult<AccountModel>(new List<ValidatorError>
					{
						new ValidatorError("Неверный пароль")
					}));
				account.Password = null;
				return Ok(new RepositoryModifyResult<AccountModel>(account));
			}
			return Ok(new RepositoryModifyResult<AccountModel>(new List<ValidatorError>
			{
				new ValidatorError("Такого пользователя не существует")
			}));
		}

		[Route("api/export")]
		[HttpGet]
		public IHttpActionResult Export()
		{
			var stream = new FileStream(HostingEnvironment.MapPath(SemanticRepositoryBase<Subject>.OntologyPath), FileMode.Open);
			var result = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StreamContent(stream)
			};
			result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
			{
				FileName = "ontology.owl"
			};
			result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
			return ResponseMessage(result);
		}
	}
}