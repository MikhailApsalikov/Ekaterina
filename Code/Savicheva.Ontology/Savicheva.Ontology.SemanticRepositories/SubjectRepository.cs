﻿namespace Savicheva.Ontology.SemanticRepositories
{
	using System.Collections.Generic;
	using Entities;
	using Helpers;
	using Interfaces;
	using Selp.Common.Entities;
	using VDS.RDF;
	using VDS.RDF.Ontology;

	public class SubjectRepository : SemanticRepositoryBase<Subject>, ISubjectRepository
	{
		public SubjectRepository(IGraphProxy graphProxy) : base(graphProxy)
		{
		}

		protected override string EntityName => "Subject";

		public RepositoryModifyResult<Subject> Create(Subject entity)
		{
			return new RepositoryModifyResult<Subject>(entity);
		}

		public RepositoryModifyResult<Subject> Update(int id, Subject entity)
		{
			return new RepositoryModifyResult<Subject>(entity);
		}

		protected override Subject Map(OntologyResource instance)
		{
			var result = new Subject
			{
				HasHourForInd = instance.GetIntProperty("hasHourForInd"),
				HasHourForKoll = instance.GetIntProperty("hasHourForKoll"),
				HasHourForLab = instance.GetIntProperty("hasHourForLab"),
				HasHourForLecture = instance.GetIntProperty("hasHourForLecture"),
				HasHourForPract = instance.GetIntProperty("hasHourForPract"),
				Title = instance.GetStringProperty("title"),
				Id = instance.GetId(),
				FormsOfControl = MapFormOfControl(instance)
			};
			return result;
		}

		private List<FormOfControl> MapFormOfControl(OntologyResource instance)
		{
			List<UriNode> formsOfControl = instance.GetObjectProperties("hasForm", GraphProxy.Graph);
			return null;
		}
	}
}