namespace Savicheva.Ontology.Entities
{
	using Selp.Interfaces;

	// Выпускающая кафедра
	public class Department : ISelpEntity<int>
	{
		public string Title { get; set; }
		public int Id { get; set; }
	}
}