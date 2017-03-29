﻿namespace Savicheva.Ontology.Web.ApiControllers
{
	using System.Collections.Generic;
	using System.Web.Http;
	using AutoMapper;
	using Entities;
	using Interfaces;
	using Models;
	using Selp.Common.Entities;
	using Selp.Controller;
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
			{
				return Ok(new RepositoryModifyResult<bool?>(validator.Errors));
			}

			List<Account> result =
				Repository.GetByCustomExpression(d => d.Name == model.Name);
			if (result.Count == 1)
			{
				if (result[0].Password != model.Password)
				{
					return Ok(new RepositoryModifyResult<bool?>(new List<ValidatorError>
					{
						new ValidatorError("Неверный пароль")
					}));
				}
				return Ok(new RepositoryModifyResult<bool?>(true));
			}
			return Ok(new RepositoryModifyResult<bool?>(new List<ValidatorError>
			{
				new ValidatorError("Такого пользователя не существует")
			}));
		}
	}
}