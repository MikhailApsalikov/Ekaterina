namespace Savicheva.Ontology.Validators
{
	using System.Linq;
	using Entities;
	using Interfaces;

	public class AccountCreateValidator : AccountValidator
	{
		private readonly IAccountRepository accountRepository;
		private readonly Account entity;

		public AccountCreateValidator(Account entity, IAccountRepository accountRepository) : base(entity)
		{
			this.entity = entity;
			this.accountRepository = accountRepository;
		}

		protected override void ValidateLogic()
		{
			if (accountRepository.GetByCustomExpression(s => s.Name == entity.Name).Any())
			{
				AddError("Пользователь с таким именем уже существует", "Имя пользователя");
			}
			base.ValidateLogic();
		}
	}
}