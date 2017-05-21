namespace Savicheva.Ontology.Entities
{
	using Selp.Interfaces;

	public class IdTitle : ISelpEntity<string>
	{
		public string Title { get; set; }
		public string Id { get; set; }
	}
}