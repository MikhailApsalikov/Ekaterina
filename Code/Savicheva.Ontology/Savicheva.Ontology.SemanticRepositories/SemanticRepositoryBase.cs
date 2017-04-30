﻿namespace Savicheva.Ontology.SemanticRepositories
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Web.Hosting;
	using Selp.Interfaces;
	using VDS.RDF;
	using VDS.RDF.Ontology;

	public abstract class SemanticRepositoryBase<TEntity> where TEntity : ISelpEntity<int>
	{
		public OntologyGraph Graph { get; private set; }

		public string Filename { get; }

		protected SemanticRepositoryBase()
		{
			Filename = Path.Combine(HostingEnvironment.MapPath("~/App_Data/Ontology.owl"));
			LoadGraph();
		}

		public virtual List<TEntity> GetAll()
		{
			OntologyClass ontClass = GetClass(EntityName);
			return ontClass.Instances.Select(Map).ToList();
		}


		public virtual TEntity GetById(int id)
		{
			return GetAll().FirstOrDefault(s => s.Id == id);
		}

		protected void LoadGraph()
		{
			Graph = new OntologyGraph();
			Graph.LoadFromFile(Filename);
		}

		protected OntologyClass GetClass(string name)
		{
			return Graph.OwlClasses.FirstOrDefault(c=>c.Resource.ToString().Contains(name));
		}

		protected abstract TEntity Map(OntologyResource instance);

		protected abstract string EntityName { get; }
	}
}