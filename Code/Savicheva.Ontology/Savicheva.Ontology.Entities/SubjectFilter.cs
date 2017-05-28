namespace Savicheva.Ontology.Entities
{
	public class SubjectFilter
	{
		public string Title { get; set; }
		public string FormsOfControl { get; set; }
		public string StudyProgramme { get; set; }
		public int? HasHourForPract { get; set; }
		public int? HasHourForLecture { get; set; }
		public int? HasHourForLab { get; set; }
		public int? HasHourForKoll { get; set; }
		public int? HasHourForInd { get; set; }
	}
}