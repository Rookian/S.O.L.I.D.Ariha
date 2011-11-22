using System.Web;
using StructureMap;

namespace DependencyResolution
{
    public class DependencyRegistrarModule : IHttpModule
    {
        private static bool _dependenciesRegistered;
        private static readonly object Lock = new object();

        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, args) => EnsureDependenciesRegistered();
        }

        public void Dispose() { }

        private static void EnsureDependenciesRegistered()
        {
            if (!_dependenciesRegistered)
            {
                lock (Lock)
                {
                    if (!_dependenciesRegistered)
                    {
                        ObjectFactory.ResetDefaults();
                        ObjectFactory.Initialize(x => x.AddRegistry(new DependencyRegistry()));
                        new InitiailizeDefaultFactories().Configure();
                        _dependenciesRegistered = true;
                    }
                }
            }
        }
    }
}