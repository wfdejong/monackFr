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
        /// Add package to packages if it has one or more modules
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
		bool IPackageManager.AddPackage(IPackage package)
        {
			if (package == null)
			{
				throw new PackageNullReferenceException("Package object reference is null");
			}

            //TODO: loading modules and context should be done somewhere else. Create and Use IsModule property or someting like that.
            package.LoadModules();
                        
            if (package.Modules.Count() > 0)
            {
                package.LoadContexts();
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
        IEnumerable<IPackage> IPackageManager.Packages
        {
            get
            {
                return _packages;
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

        IEnumerable<IContext> IPackageManager.Contexts
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

                return contexts;
            }
        }

        IEnumerable<IAuthorization> IPackageManager.Authorizations
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

				return authorizations;
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