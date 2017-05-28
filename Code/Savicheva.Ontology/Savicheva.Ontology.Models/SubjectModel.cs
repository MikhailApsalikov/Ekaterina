namespace Savicheva.Ontology.Models
{
	using System.Collections.Generic;
	using Entities;
	using Selp.Interfaces;
	public class SubjectModel : ISelpEntity<string>
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
		public IdTitle StudyProgramme { get; set; }

		public class UpdateData
		{
			public string Title { get; set; }
			public int? HasHourForPract { get; set; }
			public int? HasHourForLecture { get; set; }
			public int? HasHourForLab { get; set; }
			public int? HasHourForKoll { get; set; }
			public int? HasHourForInd { get; set; }
			public List<string> FormsOfControl { get; set; }
			public List<string> Modules { get; set; }
		}
	}
}