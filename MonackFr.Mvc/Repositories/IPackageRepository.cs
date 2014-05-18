using MonackFr.Mvc.Entities;
using MonackFr.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Repositories
{
	public interface IPackageRepository : IGenericRepository<Package>
	{
        void InstallPackages(Package[] packages);
	}
}