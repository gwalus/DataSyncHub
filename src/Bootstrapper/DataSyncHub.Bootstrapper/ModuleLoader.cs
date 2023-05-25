using DataSyncHub.Shared.Abstractions.Modules;
using System.Linq;
using System.Reflection;

namespace DataSyncHub.Bootstrapper
{
    internal static class ModuleLoader
    {
        public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
        {
            const string modulePart = "DataSyncHub.Modules.";

            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .ToList();

            var locations = assemblies
                .Where(x => !x.IsDynamic)
                .Select(x => x.Location)
                .ToList();

            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => !locations.Contains(x))
                .ToList();

            foreach (var file in files)
            {
                if (!file.Contains(modulePart))
                {
                    continue;
                }

                assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(file)));
            }

            return assemblies;
        }

        public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
        {
            var modules = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
                .OrderBy(x => x.Name)
                .Select(Activator.CreateInstance)
                .Cast<IModule>()
                .ToList(); 
            
            return modules;
        }
    }
}
