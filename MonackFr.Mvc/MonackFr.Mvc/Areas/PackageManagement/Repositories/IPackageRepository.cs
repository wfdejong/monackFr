﻿using MonackFr.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.PackageManagement.Repositories
{
	public interface IPackageRepository : IGenericRepository<Package>
	{
	}
}