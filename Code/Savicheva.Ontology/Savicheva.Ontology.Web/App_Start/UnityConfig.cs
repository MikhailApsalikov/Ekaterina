namespace Savicheva.Ontology.Web
{
	using System;
	using System.Collections.Generic;
	using System.Web.Http;
	using System.Web.Http.Dependencies;
	using Interfaces;
	using Microsoft.Practices.Unity;
	using Repositories;
	using Selp.Configuration;
	using Selp.Interfaces;
	using SemanticRepositories;

	/// <summary>
	///     Bootstrapper for the application.
	/// </summary>
	public static class UnityConfig
	{
		public static void Register(HttpConfiguration config)
		{
			var dbContext = new UserDbContext();
			var container = new UnityContainer();

			container.RegisterType<ISelpConfiguration, InMemoryConfiguration>();
			var efConstructorParameter = new InjectionConstructor(dbContext, container.Resolve<ISelpConfiguration>());

			container.RegisterType<IAccountRepository, AccountRepository>(efConstructorParameter);
			container.RegisterType<ISubjectRepository, SubjectRepository>();
			container.RegisterType<IFormsOfControlRepository, FormsOfControlRepository>();
			
			container.RegisterType<GraphProxy>(new ContainerControlledLifetimeManager());
			container.RegisterType<IGraphProxy, GraphProxy>();

			config.DependencyResolver = new UnityResolver(container);
		}
	}

	public class UnityResolver : IDependencyResolver
	{
		protected IUnityContainer container;

		public UnityResolver(IUnityContainer container)
		{
			if (container == null)
				throw new ArgumentNullException("container");
			this.container = container;
		}

		public object GetService(Type serviceType)
		{
			try
			{
				return container.Resolve(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return null;
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			try
			{
				return container.ResolveAll(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return new List<object>();
			}
		}

		public IDependencyScope BeginScope()
		{
			IUnityContainer child = container.CreateChildContainer();
			return new UnityResolver(child);
		}

		public void Dispose()
		{
			container.Dispose();
		}
	}
}