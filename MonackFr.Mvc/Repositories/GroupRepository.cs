using MonackFr.Library.Repository;
using MonackFr.Mvc.Entities;
using MonackFr.Mvc.Contexts;

namespace MonackFr.Mvc.Repositories
{
    public class GroupRepository : GenericRepository<UserManagementContext, Group>, IGroupRepository
    {
    }
}