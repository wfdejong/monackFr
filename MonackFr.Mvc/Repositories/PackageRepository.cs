using MonackFr.Mvc.Contexts;
using MonackFr.Mvc.Entities;
using MonackFr.Library.Repository;
using System.Collections.Generic;

namespace MonackFr.Mvc.Repositories
{
	public class PackageRepository : GenericRepository<PackageContext, Package>, IPackageRepository
	{
        /// <summary>
        /// Installs the packages in the database.
        /// </summary>
        /// <param name="packages"></param>
        void IPackageRepository.InstallPackages(IEnumerable<Package> packages)
        {
            foreach (Package package in packages)
            {
                this.Create(package);
            }
            this.Save();            
        }
	}
}