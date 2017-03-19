namespace Savicheva.Ontology.Entities
{
	using Selp.Interfaces;

	// профиль
	public class Profile : ISelpEntity<int>
	{
		public string Title { get; set; }
		public int Id { get; set; }
	}
}