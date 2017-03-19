namespace Savicheva.Ontology.Entities
{
	using Selp.Interfaces;

	// Направление подготовки
	public class Direction : ISelpEntity<int>
	{
		public string Title { get; set; }
		public int Id { get; set; }
	}
}