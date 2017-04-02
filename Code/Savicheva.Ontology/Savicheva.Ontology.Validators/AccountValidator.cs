namespace Savicheva.Ontology.Validators
{
	using Entities;
	using Selp.Validator;

	public class AccountValidator : SelpValidator
	{
		public AccountValidator(Account user)
		{
			User = user;
		}

		public Account User { get; set; }

		public override string EntityName => "Account";

		protected override void ValidateLogic()
		{
			if (User == null)
			{
				AddError("Ошибка валидатора: User = null");
				return;
			}

			if (string.IsNullOrWhiteSpace(User.Name))
			{
				AddError("Введите логин", "Логин");
			}
			else
			{
				if (User.Name.Length < 3)
				{
					AddError("Логин должен содержать не менее 3 символов", "Логин");
				}
				if (User.Name.Length > 50)
				{
					AddError("Логин должен содержать не более 50 символов", "Логин");
				}
			}

			if (string.IsNullOrWhiteSpace(User.Password))
			{
				AddError("Введите пароль", "Пароль");
			}
			else
			{
				if (User.Name.Length < 3)
				{
					AddError("Пароль должен содержать не менее 3 символов", "Пароль");
				}
				if (User.Name.Length > 50)
				{
					AddError("Пароль должен содержать не более 50 символов", "Пароль");
				}
			}

			if ((int) User.Role > 2 || User.Role < 0)
			{
				AddError("Ошибка назначения роли", "Роль");
			}
		}
	}
}