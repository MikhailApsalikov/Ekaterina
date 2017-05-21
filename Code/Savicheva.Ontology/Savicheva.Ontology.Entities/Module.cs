namespace Savicheva.Ontology.Entities
{
	using Selp.Interfaces;

	public class Module : ISelpEntity<string>
	{
		public string Title { get; set; }
		public string Id { get; set; }
	}
}