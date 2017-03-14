namespace Savicheva.Ontology.Entities
{
	using Enums;
	using Selp.Interfaces;

	public class Account : ISelpEntity<int>
	{
		public string Name { get; set; }

		public string Password { get; set; }

		public AccountRole Role { get; set; }

		public bool IsRemoved { get; set; }
		public int Id { get; set; }
	}
}