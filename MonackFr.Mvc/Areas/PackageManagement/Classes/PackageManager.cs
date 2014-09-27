using AutoMapper;
using MonackFr.Module;
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

		#endregion //private fields
        
        #region constructors

        public PackageManager()
        {
            _directory = new MonackFr.Wrappers.Directory();
			_iPackageManager = this as IPackageManager;			
        }

        public PackageManager(MonackFr.Wrappers.IDirectory directory, IPackageManager packageManager)
        {
            _directory = directory;
			_iPackageManager = packageManager;
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
            IPackage loadedPackage = new Loader<IPackage>(path).LoadedItems.FirstOrDefault();

            //if it implements an IPackage interface, loads IModule interface and add to package
            
            if(loadedPackage != null)
            {
                Package package = Mapper.Map<Package>(loadedPackage);

                package.RelativePath = relativePath;
                package.Contexts = new Loader<IContext>(path).LoadedItems;

                IEnumerable<IModule> imodules = new Loader<IModule>(path).LoadedItems;
                List<Module> packageModules = new List<Module>();

                foreach (IModule imodule in imodules)
                {
                    Module module = Mapper.Map<Module>(imodule);

                    if(imodule is IAuthorization)
                    {
                        IAuthorization authorization = imodule as IAuthorization;
                        module.Roles = authorization.GetRoles();
                    }

                    packageModules.Add(module);
                }

                package.Modules = packageModules;
                
                //package.Authorizations = new Loader<IAuthorization>(path).LoadedItems;

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