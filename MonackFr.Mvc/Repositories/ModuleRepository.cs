using MonackFr.Mvc.Contexts;
using MonackFr.Library.Repository;

namespace MonackFr.Mvc.Repositories
{
    public class ModuleRepository : GenericRepository<PackageContext, Entities.Module>, IModuleRepository
    {
    }
}