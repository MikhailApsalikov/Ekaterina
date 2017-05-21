using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savicheva.Ontology.SemanticRepositories
{
	using Entities;
	using Helpers;
	using Savicheva.Ontology.Interfaces;
	using VDS.RDF;
	using VDS.RDF.Ontology;

	public class ModuleRepository : SemanticRepositoryBase<IdTitle>, IModuleRepository
	{
		public ModuleRepository(IGraphProxy graphProxy) : base(graphProxy)
		{
		}

		protected override string EntityName => "Module";
		protected override IdTitle Map(OntologyResource instance)
		{
			return new IdTitle
			{
				Id = instance.GetId(),
				Title = instance.GetStringProperty("title")
			};
		}

		protected override void SetProperties(IdTitle entity, Individual instance)
		{
			throw new NotSupportedException();
		}

		public IEnumerable<UriNode> GetAllNodes()
		{
			return GetClass(EntityName).Instances.Select(s => s.Resource).Cast<UriNode>().ToList();
		}
	}
}
