using MonackFr.Mvc.Areas.PackageManagement.Contexts;
using MonackFr.Mvc.Areas.PackageManagement.Entities;
using MonackFr.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.PackageManagement.Repositories
{
	public class PackageRepository : GenericRepository<PackageContext, Package>, IPackageRepository
	{
	}
}