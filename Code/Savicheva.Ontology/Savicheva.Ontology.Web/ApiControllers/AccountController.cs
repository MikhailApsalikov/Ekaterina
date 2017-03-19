namespace Savicheva.Ontology.Web.ApiControllers
{
	using System.Collections.Generic;
	using System.Web.Http;
	using AutoMapper;
	using Entities;
	using Models;
	using Selp.Common.Entities;
	using Selp.Controller;
	using Selp.Interfaces;
	using Validators;

	public class AccountController : SelpController<AccountModel, AccountModel, Account, int>
	{
		public AccountController(ISelpRepository<Account, int> repository) : base(repository)
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
			{
				return Ok(new
				{
					valid = false,
					errors = validator.Errors
				});
			}

			List<Account> result =
				Repository.GetByCustomExpression(d => d.Id == model.Id && d.Password == model.Password);
			if (result.Count == 1)
			{
				return Ok(new { valid = true });
			}

			return Ok(new
			{
				valid = false,
				errors = new List<ValidatorError>()
				{
					new ValidatorError("Неверный логин/пароль")
				}
			});
		}
	}
}