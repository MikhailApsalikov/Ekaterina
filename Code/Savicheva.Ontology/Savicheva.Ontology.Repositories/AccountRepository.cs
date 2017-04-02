namespace Savicheva.Ontology.Repositories
{
	using System.Data.Entity;
	using System.Linq;
	using Entities;
	using Interfaces;
	using Selp.Common.Entities;
	using Selp.Interfaces;
	using Selp.Repository;
	using Validators;

	public class AccountRepository : SelpRepository<Account, int>, IAccountRepository
	{
		public AccountRepository(DbContext dbContext, ISelpConfiguration configuration) : base(dbContext, configuration)
		{
		}

		public override bool IsRemovingFake => true;
		public override string FakeRemovingPropertyName => "IsRemoved";
		public override IDbSet<Account> DbSet => ((UserDbContext)DbContext).Accounts;

		public override RepositoryModifyResult<Account> Update(int id, Account entity)
		{
			var validator = new AccountValidator(entity);
			validator.Validate();
			if (!validator.IsValid)
			{
				return new RepositoryModifyResult<Account>(validator.Errors);
			}
			return base.Update(id, entity);
		}

		protected override Account Merge(Account source, Account destination)
		{
			destination.Name = source.Name;
			destination.Role = source.Role;
			destination.Password = source.Password;
			return destination;
		}

		protected override IQueryable<Account> ApplyFilters(IQueryable<Account> entities, BaseFilter filter)
		{
			return entities;
		}
	}
}