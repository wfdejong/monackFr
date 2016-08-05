using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Repository;
using MonackFr.Mvc.Entities;
using MonackFr.Mvc.Contexts;

namespace MonackFr.Mvc.Repositories
{
    public class GroupRepository : GenericRepository<UserManagementContext, Group>, IGroupRepository
    {
    }
}