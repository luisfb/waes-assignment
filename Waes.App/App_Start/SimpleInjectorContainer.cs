using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using Waes.Core.Interfaces;

namespace Waes.App
{
    public class SimpleInjectorContainer : IDependencyResolver
    {
        private static Container _container;

        public static IDependencyResolver Initialize()
        {
            var simpleInjectorContainer = new SimpleInjectorContainer();
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            _container.Register<IDependencyResolver, SimpleInjectorContainer>(Lifestyle.Singleton);
            return simpleInjectorContainer;
        }

        public static void Verify()
        {
            _container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            _container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(_container);
        }

        public void AddTransient<TInterface, TImplementation>() where TImplementation : class, TInterface where TInterface : class
        {
            _container.Register<TInterface, TImplementation>(Lifestyle.Transient);
        }
        
        public void AddSingleton<TInterface, TImplementation>() where TImplementation : class, TInterface where TInterface : class
        {
            _container.Register<TInterface, TImplementation>(Lifestyle.Singleton);
        }

        public void AddScoped<TInterface, TImplementation>() where TImplementation : class, TInterface where TInterface : class
        {
            _container.Register<TInterface, TImplementation>(Lifestyle.Scoped);
        }

        public TInterface Resolve<TInterface>() where TInterface : class
        {
            return _container.GetInstance<TInterface>();
        }

        public void BeginScope()
        {
            throw new System.NotImplementedException();
        }
    }
}