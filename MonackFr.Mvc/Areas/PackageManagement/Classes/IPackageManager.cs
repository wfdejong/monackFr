using MonackFr.Mvc.Areas.PackageManagement.Entities;
using MonackFr.Repository;
using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.PackageManagement
{
    public interface IPackageManager
    {
        bool AddPackage(IPackage package);

        void LoadPackages();

        void InstallRoles();
        
        IPackage[] Packages { get; }

        string PackageDirectory { get; set; }

        string BaseDirectory { get; set; }

        IContext[] Contexts { get; }

        IAuthorization[] Authorizations { get; }
    }
}