namespace Savicheva.Ontology.Entities
{
	using Selp.Interfaces;
	
	public class FormOfControl : ISelpEntity<string>
	{
		public string Title { get; set; }
		public string Id { get; set; }
	}
}