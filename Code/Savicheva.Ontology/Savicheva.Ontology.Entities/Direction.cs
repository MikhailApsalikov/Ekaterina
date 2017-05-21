namespace Savicheva.Ontology.Entities
{
	using Selp.Interfaces;

	// Направление подготовки
	public class Direction : ISelpEntity<string>
	{
		public string Title { get; set; }
		public string Id { get; set; }
	}
}