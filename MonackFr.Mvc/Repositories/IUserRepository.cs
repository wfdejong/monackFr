using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Repository;
using MonackFr.Mvc.Entities;

namespace MonackFr.Mvc.Repositories
{
	public interface IUserRepository : IGenericRepository<User>
	{
		Boolean Authenticate(String userName, String password);
		String[] GetRoles(String userName);
		void AddGroupsToUser(User user, params Group[] groups);
		void RemoveAllGroupsFromUser(User user);
	}
}