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
        Package GetPackage(string path);

        IEnumerable<Package> GetPackages();

		[Obsolete("Could be removed, true")]
        void InstallRoles(IEnumerable<IAuthorization> authorizations);
        
        string PackageDirectory { get; set; }

        string BaseDirectory { get; set; }        
    }
}