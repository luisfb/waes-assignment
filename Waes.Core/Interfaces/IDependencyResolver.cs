namespace Waes.Core.Interfaces
{
    public interface IDependencyResolver
    {
        void AddTransient<TInterface, TImplementation>() where TImplementation : class, TInterface where TInterface : class;
        void AddSingleton<TInterface, TImplementation>() where TImplementation : class, TInterface where TInterface : class;
        void AddScoped<TInterface, TImplementation>() where TImplementation : class, TInterface where TInterface : class;
        TInterface Resolve<TInterface>() where TInterface : class;
        void BeginScope();

    }
}
