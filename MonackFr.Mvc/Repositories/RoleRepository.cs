using System.Collections.Generic;
using System.Linq;
using MonackFr.Repository;
using MonackFr.Mvc.Entities;
using MonackFr.Mvc.Contexts;

namespace MonackFr.Mvc.Repositories
{
	public class RoleRepository : GenericRepository<UserManagementContext, Role>, IRoleRepository
	{
		public void AddUsersToRoles(string[] userNames, string[] roleNames)
		{
			//make a list of users first so it can be reused
			List<User> users = new List<User>();
			
			foreach (string userName in userNames)
			{
				users.Add(this.Entities.Users.FirstOrDefault<User>(u => u.UserName == userName));
			}			

			//add users to each role
			foreach (string roleName in roleNames)
			{
				this.Entities.Roles.Include("Users");

				Role role = this.GetSingle(r => r.Name == roleName);
				this.Entities.Roles.Include("Users");
				
				if (role.Users == null)
				{
					role.Users = new List<User>();
				}

				foreach (User user in users)
				{					
					role.Users.Add(user);
				}

				this.Edit(role);
			}
		}

		public void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
		{
			//TODO: make function without loops: 1 query should be sufficient.
			//make a list of users first so it can be reused
			List<User> users = new List<User>();

			foreach (string userName in userNames)
			{
				users.Add(this.Entities.Users.FirstOrDefault<User>(u => u.UserName == userName));
			}

			//add users to each role
			foreach (string roleName in roleNames)
			{
				Role role = this.GetSingle(r => r.Name == roleName);
				foreach (User user in users)
				{
					role.Users.Remove(user);
				}
				this.Edit(role);
			}
		}
	}
}