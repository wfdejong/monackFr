using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Mvc.Areas.UserManagement.Models;
using MonackFr.Repository;
using System.Data.Entity;

namespace MonackFr.Mvc.Areas.UserManagement.Repositories
{
	public class UserRepository : GenericRepository<UserManagementContext, User>, IUserRepository
	{		
		/// <summary>
		/// Edit user
		/// </summary>
		/// <param name="user">User to edit</param>
		public override void Edit(User user)
		{
			user.LastUpdate = DateTime.Now;			
			base.Edit(user);
		}

		/// <summary>
		/// Authenticates username and password.
		/// </summary>
		/// <param name="userName">username</param>
		/// <param name="password">password</param>
		/// <returns></returns>
		public Boolean Authenticate(String userName, String password)
		{
			User user = Entities.Users.FirstOrDefault<User>(u => u.UserName == userName && u.Password == password);

			return (user != null);
		}

		/// <summary>
		/// Returns all roles of the user
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		public String[] GetRoles(String userName)
		{			
			IQueryable<User> users = Entities.Users.Include(u => u.Roles);
			User user = users.FirstOrDefault<User>(u => u.UserName == userName);
			
			IEnumerable<String> roles = from r in user.Roles
						select r.Name;

			//TODO: eager loading
			foreach (Group userGroup in user.Groups)
			{
				IEnumerable<string> groupRoles = from r in userGroup.Roles select r.Name;
				roles = roles.Union(groupRoles);
			}
			return roles.ToArray();
		}

		public void AddGroupsToUser(User user, params Group[] groups)
		{
			for(int i=0; i<groups.Length; i++)
			{
				int groupId = groups[i].Id; //Linq to entities can't handle array references
				Group group = Entities.Groups.FirstOrDefault<Group>(g => g.Id == groupId);
				user.Groups.Add(group);
				this.Save();
			}
		}

		public void RemoveAllGroupsFromUser(User user)
		{
			Group[] groups = user.Groups.ToArray<Group>();

			for (int i=0; i<groups.Length; i++)
			{
				Group group = groups[i];
				user.Groups.Remove(group);
			}

			this.Save();
		}
	}
}