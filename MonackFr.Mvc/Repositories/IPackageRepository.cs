using MonackFr.Mvc.Entities;
using MonackFr.Library.Repository;
using System.Collections.Generic;

namespace MonackFr.Mvc.Repositories
{
	public interface IPackageRepository : IGenericRepository<Package>
	{
        void InstallPackages(IEnumerable<Package> packages);
	}
}