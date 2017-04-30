namespace Savicheva.Ontology.SemanticRepositories
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Hosting;
	using Helpers;
	using Interfaces;
	using Selp.Interfaces;
	using VDS.RDF;
	using VDS.RDF.Ontology;

	public abstract class SemanticRepositoryBase<TEntity> where TEntity : ISelpEntity<int>
	{
		protected IGraphProxy GraphProxy { get; }

		protected SemanticRepositoryBase(IGraphProxy graphProxy)
		{
			GraphProxy = graphProxy;
			GraphProxy.LoadGraph();
		}

		protected abstract string EntityName { get; }

		public virtual List<TEntity> GetAll()
		{
			OntologyClass ontClass = GetClass(EntityName);
			return ontClass.Instances.Select(Map).ToList();
		}


		public virtual TEntity GetById(int id)
		{
			return GetAll().FirstOrDefault(s => s.Id == id);
		}

		public virtual void Remove(int id)
		{
			OntologyResource instance = GetClass(EntityName).Instances.FirstOrDefault(s => s.GetId() == id);
			if (instance == null)
				return;
			GraphProxy.Graph.Retract(instance.Triples.ToList());
			GraphProxy.SaveChanges();
		}

		protected OntologyClass GetClass(string name)
		{
			return GraphProxy.Graph.OwlClasses.FirstOrDefault(c => c.Resource.ToString().Contains(name));
		}

		protected abstract TEntity Map(OntologyResource instance);
	}
}