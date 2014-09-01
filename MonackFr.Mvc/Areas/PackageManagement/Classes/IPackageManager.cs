using MonackFr.Module;
using MonackFr.Repository;
using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.PackageManagement
{
	/// <summary>
	/// Manages concrete packages
	/// </summary>
    public interface IPackageManager
    {
		/// <summary>
		/// Loades and returns a package
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
        Package GetPackage(string path);

		/// <summary>
		/// Scans directory for dll files and, if it is a package, adds them to packages
		/// </summary>
		/// <returns></returns>
        IEnumerable<Package> GetPackages();

		/// <summary>
		/// path to package locations
		/// </summary
        string PackageDirectory { get; set; }

		/// <summary>
		/// root directory of application
		/// </summary>
        string BaseDirectory { get; set; }        
    }
}