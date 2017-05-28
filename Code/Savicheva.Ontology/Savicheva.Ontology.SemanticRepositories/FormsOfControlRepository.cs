namespace Savicheva.Ontology.SemanticRepositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Entities;
	using Helpers;
	using Interfaces;
	using VDS.RDF;
	using VDS.RDF.Ontology;

	public class FormsOfControlRepository : SemanticRepositoryBase<IdTitle>, IFormsOfControlRepository
	{
		public FormsOfControlRepository(IGraphProxy graphProxy) : base(graphProxy)
		{
		}

		protected override string EntityName => "FormOfControl";

		protected override IdTitle Map(OntologyResource instance)
		{
			return MapIdTitle(instance);
		}

		protected override void SetProperties(IdTitle entity, Individual instance)
		{
			throw new NotSupportedException();
		}

		public IEnumerable<UriNode> GetAllNodes()
		{
			return GetClass(EntityName).Instances.Select(s=>s.Resource).Cast<UriNode>().ToList();
		}
	}
}