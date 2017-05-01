namespace Savicheva.Ontology.Models
{
	using System.Collections.Generic;
	using Entities;
	using Selp.Interfaces;
	public class SubjectModel : ISelpEntity<int>
	{
		public string Title { get; set; }

		public int? HasHourForPract { get; set; }

		public int? HasHourForLecture { get; set; }

		public int? HasHourForLab { get; set; }

		public int? HasHourForKoll { get; set; }

		public int? HasHourForInd { get; set; }

		public List<FormOfControl> FormsOfControl { get; set; }
		public int Id { get; set; }

		public class UpdateData
		{
			public string Title { get; set; }
			public int? HasHourForPract { get; set; }
			public int? HasHourForLecture { get; set; }
			public int? HasHourForLab { get; set; }
			public int? HasHourForKoll { get; set; }
			public int? HasHourForInd { get; set; }
			public List<int> FormsOfControl { get; set; }
		}
	}
}