using System.Collections.Generic;
using System.IO;
using System.Linq;
using MonackFr.Library.Module;
using MonackFr.Library.Repository;
using MonackFr.Security;

namespace MonackFr.Mvc.PackageManagement
{
    internal class PackageManager : IPackageManager
    {
        /// <summary>
        /// Loads classes with IPackage interface
        /// </summary>
        private readonly ILoader<IPackage> _packageLoader;

        /// <summary>
        /// Loads classes with IModule interface
        /// </summary>
        private readonly ILoader<IModule> _moduleLoader;

        /// <summary>
        /// Loads classes with IContext interface
        /// </summary>
        private readonly ILoader<IContext> _contextLoader;

        private readonly Wrappers.IDirectory _directory;

        public PackageManager()
        {
            _packageLoader = new Loader<IPackage>();
            _moduleLoader = new Loader<IModule>();
            _contextLoader = new Loader<IContext>();
            _directory = new Wrappers.Directory();
        }

        /// <summary>
        /// Returns a package if it inplements IPackage interface. 
        /// Also loads Contexts, Modules and Authorizations to this package
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <param name="baseDirectory">the base directory of the application</param>
        /// <returns>a new package</returns>
        private Package GetPackage(string path, string baseDirectory)
        {
            var mapper = AutoMapperConfig.Mapper;
            string relativePath = path.Substring(baseDirectory.Length);

            //Load IPackage interface of package
            IPackage loadedPackage = _packageLoader.Load(path).LoadedItems.SingleOrDefault();

            if (loadedPackage == null)
                return null;

            //if it implements an IPackage interface, loads IModule interface and add to packages            

            Package package = mapper.Map<Package>(loadedPackage);

            package.Path = relativePath;

            package.Contexts = _contextLoader.Load(path).LoadedItems;

            IEnumerable<IModule> imodules = _moduleLoader.Load(path).LoadedItems;
            List<Module> packageModules = new List<Module>();

            foreach (IModule imodule in imodules)
            {
                Module module = mapper.Map<Module>(imodule);

                if (imodule is IAuthorization)
                {
                    IAuthorization authorization = imodule as IAuthorization;
                    module.Roles = authorization.GetRoles();
                }

                packageModules.Add(module);
            }

            package.Modules = packageModules;

            return package;
        }

        /// <summary>
        /// Scans directory for dll files and, if it is a package, adds them to packages
        /// </summary>
        /// <param name="packageDirectory">path to directory</param>
        /// <param name="baseDirectory">base path to make relative path</param>
        IEnumerable<Package> IPackageManager.GetPackages(string packageDirectory, string baseDirectory)
        {
            List<Package> packages = new List<Package>();

            string[] files = _directory.GetFiles(packageDirectory, "*.dll", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                Package package = GetPackage(file, baseDirectory);

                //for each library, load and add package.                
                if (package == null)
                    continue;

                packages.Add(package);
            }

            return packages;
        }
    }
}