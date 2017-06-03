namespace Savicheva.Ontology.SemanticRepositories
{
	using System;
	using Entities;
	using Interfaces;
	using VDS.RDF.Ontology;

	public class ProfileRepository : SemanticRepositoryBase<IdTitle>, IProfileRepository
	{
		public ProfileRepository(IGraphProxy graphProxy) : base(graphProxy)
		{
		}

		protected override string EntityName => "Profile";

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