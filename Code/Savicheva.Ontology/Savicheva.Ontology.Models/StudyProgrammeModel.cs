﻿namespace Savicheva.Ontology.Models
{
	using System.Collections.Generic;
	using Entities;
    using Selp.Interfaces;

    public class StudyProgrammeModel : ISelpEntity<string>
    {
        public string Title { get; set; }
		
        public IdTitle Direction { get; set; }
		
        public IdTitle Department { get; set; }
		
        public IdTitle Profile { get; set; }

        public List<SubjectModel> Subjects { get; set; }

        public string Id { get; set; }

	    public class UpdateData
	    {
			public string Title { get; set; }
			
			public string Direction { get; set; }
			
			public string Department { get; set; }
			
			public string Profile { get; set; }

		    public List<string> Subjects { get; set; }

			public string Id { get; set; }
		}
    }
}