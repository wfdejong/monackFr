using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Mvc.Areas.UserManagement.Models;
using MonackFr.Repository;

namespace MonackFr.Mvc.Areas.UserManagement.Repositories
{
	public interface IRoleRepository : IGenericRepository<Role>
	{
		void AddUsersToRoles(String[] userNames, String[] roleNames);
		void RemoveUsersFromRoles(String[] userNames, String[] roleNames);
	}
}