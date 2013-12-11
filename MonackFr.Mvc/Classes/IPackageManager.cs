using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc
{
    public interface IPackageManager
    {
        bool AddPackage(IPackage package);

        void LoadPackages(string path, string basePath);

        IPackage[] Packages
        { get; }        
    }
}