namespace Savicheva.Ontology.Models
{
	using Entities.Enums;
	using Selp.Interfaces;

	public class AccountModel : ISelpEntity<int>
	{
		public string Name { get; set; }

		public string Password { get; set; }

		public AccountRole Role { get; set; }

		public bool IsRemoved { get; set; }
		public int Id { get; set; }
	}
}