using Waes.Core.Interfaces;
using Waes.Core.Models;
using Waes.Infrastructure.Repositories;
using Waes.WaesDiff;

namespace Waes.App
{
    public class IoCConfig
    {
        private static IDependencyResolver _dependencyResolver;

        private static void Initialize()
        {
            _dependencyResolver = SimpleInjectorContainer.Initialize();
        }
        private static void Verify()
        {
            SimpleInjectorContainer.Verify();
        }

        public static void RegisterDependencies()
        {
            Initialize();

            _dependencyResolver.AddTransient<IInMemoryRepository, RedisInMemoryRepository>();
            //In case you don't want to use Redis or don't have it installed, comment the above line and uncomment the line bellow
            //_dependencyResolver.AddTransient<IInMemoryRepository, InMemoryRepository>();
            _dependencyResolver.AddTransient<IDiffService, DiffService>();
            _dependencyResolver.AddTransient<IDiffResultRepository, DiffResultRepository>();
            
            Verify();
        }
    }
}