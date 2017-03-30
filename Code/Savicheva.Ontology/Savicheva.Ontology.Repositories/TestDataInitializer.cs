namespace Savicheva.Ontology.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using Entities;
	using Entities.Enums;

	public class TestDataInitializer : DropCreateDatabaseIfModelChanges<UserDbContext>
	{
		private readonly Random random = new Random();

		protected override void Seed(UserDbContext context)
		{
			InitializeTestAccounts(context);
		}

		private void InitializeTestAccounts(UserDbContext context)
		{
			if (context.Accounts.Any())
			{
				return;
			}

			var accounts = new List<Account>
			{
				new Account
				{
					Name = "admin",
					Password = "admin",
					Role = AccountRole.Admin
				},
				new Account
				{
					Name = "expert",
					Password = "expert",
					Role = AccountRole.Expert
				},
				new Account
				{
					Name = "user",
					Password = "user",
					Role = AccountRole.User
				},
				new Account
				{
					Name = "inactive",
					Password = "inactive",
					Role = AccountRole.User,
					IsRemoved = true
				}
			};

			context.Accounts.AddRange(accounts);
		}
	}
}