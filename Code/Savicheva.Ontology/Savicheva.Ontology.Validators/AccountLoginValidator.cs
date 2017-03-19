using System;

namespace Savicheva.Ontology.Validators
{
	using Models;
	using Selp.Validator;
	public class AccountLoginValidator : SelpValidator
	{
		public AccountModel User { get; set; }
		public AccountLoginValidator(AccountModel user)
		{
			User = user;
		}

		protected override void ValidateLogic()
		{
			if (User == null)
			{
				AddError("Неверный логин/пароль");
				return;
			}

			if (string.IsNullOrWhiteSpace(User.Name))
			{
				AddError("Введите логин", "Id");
			}

			if (string.IsNullOrWhiteSpace(User.Password))
			{
				AddError("Введите пароль", "Password");
			}
		}

		public override string EntityName => "Account";
	}
}
