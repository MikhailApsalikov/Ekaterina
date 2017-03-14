namespace Savicheva.Ontology.Repositories
{
	using System.Data.Entity;
	using Entities;

	public class UserDbContext : DbContext
	{
		public DbSet<Account> Accounts { get; set; }
	}
}