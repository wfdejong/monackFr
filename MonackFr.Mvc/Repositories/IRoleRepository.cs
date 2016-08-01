using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Repository;
using MonackFr.Mvc.Entities;

namespace MonackFr.Mvc.Repositories
{
	public interface IRoleRepository : IGenericRepository<Role>
	{
		void AddUsersToRoles(String[] userNames, String[] roleNames);
		void RemoveUsersFromRoles(String[] userNames, String[] roleNames);
	}
}