namespace Savicheva.Ontology.SemanticRepositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.UI.WebControls;
	using Entities;
	using Helpers;
	using Interfaces;
	using Selp.Interfaces;
	using VDS.RDF;
	using VDS.RDF.Ontology;

	public abstract class SemanticRepositoryBase<TEntity> where TEntity : ISelpEntity<string>
	{
		private const string NamedIndividual = @"http://www.w3.org/2002/07/owl#NamedIndividual";

		protected SemanticRepositoryBase(IGraphProxy graphProxy)
		{
			GraphProxy = graphProxy;
			GraphProxy.LoadGraph();
		}

		protected IGraphProxy GraphProxy { get; }

		protected abstract string EntityName { get; }

		public virtual List<TEntity> GetAll()
		{
			OntologyClass ontClass = GetClass(EntityName);
			return ontClass.Instances.Select(Map).ToList();
		}

		public virtual TEntity GetById(string id)
		{
			return GetAll().FirstOrDefault(s => s.Id == id);
		}

		public string Create(TEntity entity)
		{
			var id = GraphProxy.GenerateId();
			CreateNew(id);
			Update(id, entity);
			return id;
		}

		public void Update(string id, TEntity entity)
		{
			Individual individual = GraphProxy.Graph.CreateIndividual(new Uri(new Uri(SemanticRepositories.GraphProxy.IndividualsDomain), id.ToString()));
			SetProperties(entity, individual);
			GraphProxy.SaveChanges();
		}

		public virtual void Remove(string id)
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

		protected void CreateNew(string id)
		{
			IUriNode newUri = GraphProxy.Graph.CreateUriNode(new Uri(id));
			INode type = GraphProxy.Graph.CreateUriNode(new Uri(OntologyHelper.PropertyType));
			INode namedInvididualClass = GraphProxy.Graph.CreateUriNode(new Uri(NamedIndividual));
			OntologyClass ontologyClass = GetClass(EntityName);
			GraphProxy.Graph.Assert(new Triple(newUri, type, ontologyClass.Resource, GraphProxy.Graph));
			GraphProxy.Graph.Assert(new Triple(newUri, type, namedInvididualClass, GraphProxy.Graph));
		}

		protected IdTitle MapIdTitle(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return null;
			}
			return MapIdTitle(GraphProxy.Graph.CreateOntologyResource(new Uri(id)));
		}

		protected IdTitle MapIdTitle(OntologyResource instance)
		{
			if (instance == null)
			{
				return null;
			}
			return new IdTitle
			{
				Id = instance.GetId(),
				Title = instance.GetStringProperty("title")
			};
		}

		protected abstract TEntity Map(OntologyResource instance);
		protected abstract void SetProperties(TEntity entity, Individual instance);
	}
}