using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Repository;
using MonackFr.Mvc.Entities;

namespace MonackFr.Mvc.Repositories
{
	public interface IGroupRepository : IGenericRepository<Group>
	{		
		void AddRolesToGroup(Group group, params Role[] Roles);
		void RemoveAllRolesFromGroup(Group group);
		void AddUserToGroup(int userId, int groupId);
		void RemoveUserFromGroup(int groupId, int userId);
	}
}