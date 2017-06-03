namespace Savicheva.Ontology.SemanticRepositories
{
	using System;
	using Entities;
	using Interfaces;
	using VDS.RDF.Ontology;

	public class DirectionRepository : SemanticRepositoryBase<IdTitle>, IDirectionRepository
	{
		public DirectionRepository(IGraphProxy graphProxy) : base(graphProxy)
		{
		}

		protected override string EntityName => "NapravleniePodgotovki";

		protected override IdTitle Map(OntologyResource instance)
		{
			return MapIdTitle(instance);
		}

		protected override void SetProperties(IdTitle entity, Individual instance)
		{
			throw new NotSupportedException();
		}
	}
}