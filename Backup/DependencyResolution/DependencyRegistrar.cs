using StructureMap;

namespace DependencyResolution
{
    public class DependencyRegistrar
    {
        private static void RegisterDependencies()
        {
            // Register all dependencies given in the DependencyRegistry class
            ObjectFactory.Initialize(x => x.AddRegistry(new DependencyRegistry()));

            // Initiliaze Factories
            new InitiailizeDefaultFactories().Configure();
        }

        public void ConfigureOnStartup()
        {
            RegisterDependencies();
        }
    }
}