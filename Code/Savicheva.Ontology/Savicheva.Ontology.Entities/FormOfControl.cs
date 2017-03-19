namespace Savicheva.Ontology.Entities
{
	using Selp.Interfaces;
	
	public class FormOfControl : ISelpEntity<int>
	{
		public string Title { get; set; }
		public int Id { get; set; }
	}
}