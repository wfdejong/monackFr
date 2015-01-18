using AutoMapper;
using MonackFr.Repository;
using MonackFr.Mvc.Areas.PackageManagement.Entities;
using MonackFr.Mvc.Repositories;
using MonackFr.Repository;
using MonackFr.Security;
using MonackFr.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.PackageManagement
{
	/// <summary>
	/// Manages packages
	/// </summary>
    public class PackageManager : IPackageManager
    {
		#region private fields

		/// <summary>
		/// interface to directory
		/// </summary>
		private MonackFr.Wrappers.IDirectory _directory = null;

		/// <summary>
		/// the packagemanager
		/// </summary>
		private IPackageManager _iPackageManager = null;

		/// <summary>
		/// The module loader
		/// </summary>
		private ILoader<IModule> _moduleLoader = null;

		/// <summary>
		/// The context loader
		/// </summary>
		private ILoader<IContext> _contextLoader = null;

		/// <summary>
		/// The package loader
		/// </summary>
		private ILoader<IPackage> _packageLoader = null;

		/// <summary>
		/// The Automapper
		/// </summary>
		private IMappingEngine _mapper = null;

		#endregion //private fields
        
        #region constructors

        public PackageManager()
        {
            _directory = new MonackFr.Wrappers.Directory();
			_iPackageManager = this as IPackageManager;
			_packageLoader = new Loader<IPackage>();
			_moduleLoader = new Loader<IModule>();
			_contextLoader = new Loader<IContext>();
			_mapper = AutoMapper.Mapper.Engine;			
        }

        public PackageManager(MonackFr.Wrappers.IDirectory directory, IPackageManager packageManager, ILoader<IModule> moduleLoader, ILoader<IContext> contextLoader, ILoader<IPackage> packageLoader, IMappingEngine mapper)
        {
			//TODO: refactor to a injection class instead of so many parameters
            _directory = directory;
			_iPackageManager = packageManager;
			_moduleLoader = moduleLoader;
			_contextLoader = contextLoader;
			_packageLoader = packageLoader;
			_mapper = mapper;
        }

        #endregion //constructors

        #region IPackageManager

		/// <summary>
		/// Package directory
		/// </summary>
		string IPackageManager.PackageDirectory { get; set; }

		/// <summary>
		/// Base directory to create relative path
		/// </summary>
		string IPackageManager.BaseDirectory { get; set; }

        /// <summary>
        /// Returns a package if it inplements IPackage interface. 
		/// Also loads Contexts, Modules and Authorizations to this package
        /// </summary>
        /// <param name="package"></param>
        /// <returns>a new package</returns>
		Package IPackageManager.GetPackage(string path)
        {
            string relativePath = path.Substring(_iPackageManager.BaseDirectory.Count());

            //Load IPackage interface of package
			IPackage loadedPackage = _packageLoader.Load(path).LoadedItems.FirstOrDefault();

            //if it implements an IPackage interface, loads IModule interface and add to packages            
            if(loadedPackage != null)
            {
                Package package = _mapper.Map<Package>(loadedPackage);

                package.RelativePath = relativePath;

				package.Contexts = _contextLoader.Load(path).LoadedItems;

				IEnumerable<IModule> imodules = _moduleLoader.Load(path).LoadedItems;
                List<Module> packageModules = new List<Module>();

                foreach (IModule imodule in imodules)
                {
                    Module module = _mapper.Map<Module>(imodule);

                    if(imodule is IAuthorization)
                    {
                        IAuthorization authorization = imodule as IAuthorization;
                        module.Roles = authorization.GetRoles();
                    }

                    packageModules.Add(module);
                }

                package.Modules = packageModules;
                
                return package;
            }

            return null;
        }

        /// <summary>
        /// Scans directory for dll files and, if it is a package, adds them to packages
        /// </summary>
        /// <param name="path">path to directory</param>
        /// <param name="basePath">base path to make relative path</param>
        IEnumerable<Package> IPackageManager.GetPackages()
        {
            List<Package> packages = new List<Package>();

            string[] files = _directory.GetFiles(_iPackageManager.PackageDirectory, "*.dll", SearchOption.AllDirectories);

            foreach (string file in files)
            {                
                Package package = _iPackageManager.GetPackage(file);

                //for each library, load and add package.                
                if (package != null)
                {
                    packages.Add(package);
                }
            }

            return packages;
        }
				        
        #endregion //IPackageManager
    }
}