using MonackFr.Mvc.Contexts;
using MonackFr.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Repositories
{
    public class ModuleRepository : GenericRepository<PackageContext, Entities.Module>, IModuleRepository
    {
    }
}