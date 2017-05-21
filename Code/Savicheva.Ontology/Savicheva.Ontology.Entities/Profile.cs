namespace Savicheva.Ontology.Entities
{
	using Selp.Interfaces;

	// профиль
	public class Profile : ISelpEntity<string>
	{
		public string Title { get; set; }
		public string Id { get; set; }
	}
}