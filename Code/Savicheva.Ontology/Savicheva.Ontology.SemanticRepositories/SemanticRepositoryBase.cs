﻿namespace Savicheva.Ontology.SemanticRepositories
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Hosting;
	using Helpers;
	using Selp.Interfaces;
	using VDS.RDF;
	using VDS.RDF.Ontology;

	public abstract class SemanticRepositoryBase<TEntity> where TEntity : ISelpEntity<int>
	{
		public const string OntologyPath = "~/App_Data/Ontology.owl";

		protected SemanticRepositoryBase()
		{
			Filename = HostingEnvironment.MapPath(OntologyPath);
			LoadGraph();
		}

		public OntologyGraph Graph { get; private set; }

		public string Filename { get; }

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
			Graph.Retract(instance.Triples.ToList());
			SaveChanges();
		}

		protected void LoadGraph()
		{
			Graph = new OntologyGraph();
			Graph.LoadFromFile(Filename);
		}


		protected void SaveChanges()
		{
			Graph.SaveToFile(Filename);
		}

		protected OntologyClass GetClass(string name)
		{
			return Graph.OwlClasses.FirstOrDefault(c => c.Resource.ToString().Contains(name));
		}

		protected abstract TEntity Map(OntologyResource instance);
	}
}