namespace Savicheva.Ontology.Entities
{
	using System.Collections.Generic;
	using Selp.Interfaces;

	public class Subject : ISelpEntity<string>
	{
		public string Title { get; set; }

		public int? HasHourForPract { get; set; }

		public int? HasHourForLecture { get; set; }

		public int? HasHourForLab { get; set; }

		public int? HasHourForKoll { get; set; }

		public int? HasHourForInd { get; set; }

		public List<IdTitle> FormsOfControl { get; set; }
		public List<IdTitle> Modules { get; set; }
		public string Id { get; set; }
	}
}