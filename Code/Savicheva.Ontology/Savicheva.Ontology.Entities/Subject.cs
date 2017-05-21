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

		public List<FormOfControl> FormsOfControl { get; set; }
		public List<Module> Modules { get; set; }
		public string Id { get; set; }
	}
}