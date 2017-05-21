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

	public class ModuleRepository : SemanticRepositoryBase<Module>, IModuleRepository
	{
		public ModuleRepository(IGraphProxy graphProxy) : base(graphProxy)
		{
		}

		protected override string EntityName => "Module";
		protected override Module Map(OntologyResource instance)
		{
			return new Module
			{
				Id = instance.GetId(),
				Title = instance.GetStringProperty("title")
			};
		}

		protected override void SetProperties(Module entity, Individual instance)
		{
			throw new NotSupportedException();
		}

		public IEnumerable<UriNode> GetAllNodes()
		{
			return GetClass(EntityName).Instances.Select(s => s.Resource).Cast<UriNode>().ToList();
		}
	}
}
