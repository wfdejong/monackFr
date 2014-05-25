using MonackFr.Module;
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
        
        IEnumerable<IPackage> Packages { get; }

        string PackageDirectory { get; set; }

        string BaseDirectory { get; set; }

        IEnumerable<IContext> Contexts { get; }

        IEnumerable<IAuthorization> Authorizations { get; }
    }
}