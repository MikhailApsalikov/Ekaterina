namespace Savicheva.Ontology.Entities
{
	using System.Collections.Generic;
	using Selp.Interfaces;

	public class StudyProgramme : ISelpEntity<string>
	{
		//hasNapr
		public IdTitle Direction { get; set; }

		//hasDepartment
		public IdTitle Department { get; set; }

		//hasProfile
		public IdTitle Profile { get; set; }

		//hasSubject
		public List<Subject> Subjects { get; set; }

		public string Title { get; set; }
		public string Id { get; set; }
	}
}