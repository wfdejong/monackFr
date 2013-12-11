using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc
{
    public class PackageManager : IPackageManager
    {
        private List<IPackage> _packages = new List<IPackage>();
        private MonackFr.Wrappers.IDirectory _directory = null;

        #region constructors

        public PackageManager()
        {
            _directory = new MonackFr.Wrappers.Directory();
        }

        public PackageManager(MonackFr.Wrappers.IDirectory directory)
        {
            _directory = directory;
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
        void IPackageManager.LoadPackages(string path, string basePath)
        {
            string[] files = _directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                string relativePath = file.Substring(basePath.Count());
                IPackage package = new Package(relativePath);

                IPackageManager packageManager = (IPackageManager)this;
                packageManager.AddPackage(package);
            }
        }

        IPackage[] IPackageManager.Packages
        {
            get
            {
                return _packages.ToArray();
            }
        }

        #endregion //IPackageManager
    }
}