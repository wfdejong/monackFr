using MonackFr.Mvc.Areas.PackageManagement.Entities;
using MonackFr.Mvc.Areas.PackageManagement.Repositories;
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
    public class PackageManager : IPackageManager
    {
        private IList<IPackage> _packages = null;
        private MonackFr.Wrappers.IDirectory _directory = null;
        private IPackageManager _iPackageManager = null;
		private IRoles _roles = null;

        #region constructors

        public PackageManager()
        {
            _directory = new MonackFr.Wrappers.Directory();
			_packages = new List<IPackage>();
            _iPackageManager = this as IPackageManager;
			_roles = new Roles();
        }

        public PackageManager(MonackFr.Wrappers.IDirectory directory, IList<IPackage> packages, IPackageManager packageManager, IRoles roles)
        {
            _directory = directory;
			_packages = packages;
			_iPackageManager = packageManager;
			_roles = roles;
        }

        #endregion //constructors


        #region IPackageManager

        /// <summary>
        /// Add package to packages if it at lease one context and one module
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
		bool IPackageManager.AddPackage(IPackage package)
        {
			if (package == null)
			{
				throw new PackageNullReferenceException("Package object reference is null");
			}
            package.LoadContexts();
            package.LoadModules();

            if (package.Contexts.Count() > 0 && package.Modules.Count() > 0)
            {
                _packages.Add(package);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Scans directory for dll files and, if it is a package, adds them to packages
        /// </summary>
        /// <param name="path">path to directory</param>
        /// <param name="basePath">base path to make relative path</param>
        void IPackageManager.LoadPackages()
        {
            string[] files = _directory.GetFiles(_iPackageManager.PackageDirectory, "*.dll", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                string relativePath = file.Substring(_iPackageManager.BaseDirectory.Count());
                IPackage package = new Package(relativePath);
				_iPackageManager.AddPackage(package);
            }
        }

        /// <summary>
        /// Loaded packages
        /// </summary>
        IPackage[] IPackageManager.Packages
        {
            get
            {
                return _packages.ToArray();
            }
        }

        /// <summary>
        /// Package directory
        /// </summary>
        string IPackageManager.PackageDirectory { get; set; }

        /// <summary>
        /// Base directory to create relative path
        /// </summary>
        string IPackageManager.BaseDirectory { get; set; }

        IContext[] IPackageManager.Contexts
        {
            get
            {
                List<IContext> contexts = new List<IContext>();

                foreach (IPackage package in _iPackageManager.Packages)
                {
                    if (package.Contexts != null)
                    {
                        contexts.AddRange(package.Contexts);
                    }
                }

                return contexts.ToArray();
            }
        }

        IAuthorization[] IPackageManager.Authorizations
        {
            get
            {
                List<IAuthorization> authorizations = new List<IAuthorization>();

                foreach (IPackage package in _iPackageManager.Packages)
                {
                    package.LoadAuthorizations();

                    if (package.Authorizations != null)
                    {
                        authorizations.AddRange(package.Authorizations);
                    }
                }

				return authorizations.ToArray();
            }
        }

        /// <summary>
        /// Install all roles defined in the packages
        /// </summary>
        void IPackageManager.InstallRoles()
        {
			foreach (IAuthorization authorization in _iPackageManager.Authorizations)
			{
				IEnumerable<IMfrRole> roles = authorization.GetRoles();

				foreach (IMfrRole role in roles)
				{
					_roles.CreateRole(role);
				}
			}            
        }
        
        #endregion //IPackageManager
    }
}