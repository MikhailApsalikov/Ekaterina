namespace Savicheva.Ontology.SemanticRepositories
{
	using Entities;
	using Helpers;
	using Interfaces;
	using VDS.RDF.Ontology;

	public class FormsOfControlRepository : SemanticRepositoryBase<FormOfControl>, IFormsOfControlRepository
	{
		public FormsOfControlRepository(IGraphProxy graphProxy) : base(graphProxy)
		{
		}

		protected override string EntityName => "FormOfControl";

		protected override FormOfControl Map(OntologyResource instance)
		{
			return new FormOfControl
			{
				Id = instance.GetId(),
				Title = instance.GetStringProperty("title")
			};
		}
	}
}