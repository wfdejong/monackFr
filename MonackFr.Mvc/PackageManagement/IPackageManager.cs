using System.Collections.Generic;

namespace MonackFr.Mvc.PackageManagement
{
    /// <summary>
    /// Manages concrete packages
    /// </summary>
    internal interface IPackageManager
    {
        /// <summary>
        /// Returns all packages defined in .dll files located in PackageDirectory
        /// </summary>
        /// <returns></returns>
        IEnumerable<Package> GetPackages(string packageDirectory, string baseDirectory);
    }
}