namespace Savicheva.Ontology.Validators
{
	using Models;
	using Selp.Validator;

	public class AccountLoginValidator : SelpValidator
	{
		public AccountLoginValidator(AccountModel user)
		{
			User = user;
		}

		public AccountModel User { get; set; }

		public override string EntityName => "Account";

		protected override void ValidateLogic()
		{
			if (User == null)
			{
				AddError("Введите имя пользователя и пароль");
				return;
			}

			if (string.IsNullOrWhiteSpace(User.Name))
			{
				AddError("Введите имя пользователя", "Имя пользователя");
			}

			if (string.IsNullOrWhiteSpace(User.Password))
			{
				AddError("Введите пароль", "Пароль");
			}
		}
	}
}