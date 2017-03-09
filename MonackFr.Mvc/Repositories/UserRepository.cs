using System;
using System.Collections.Generic;
using System.Linq;
using MonackFr.Library.Repository;
using System.Data.Entity;
using System.Linq.Expressions;
using MonackFr.Mvc.Entities;
using MonackFr.Mvc.Contexts;

namespace MonackFr.Mvc.Repositories
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

	    public override User GetSingle(Expression<Func<User, bool>> predicate)
	    {
	        var user = base.GetSingle(predicate);
	        Entities.Entry(user).Collection(u => u.Groups).Load();
	        return user;
	    }

	    /// <summary>
		/// Authenticates username and password.
		/// </summary>
		/// <param name="userName">username</param>
		/// <param name="password">password</param>
		/// <returns></returns>
		public bool Authenticate(string userName, string password)
		{
			User user = Entities.Users.FirstOrDefault<User>(u => u.UserName == userName && u.Password == password);

			return user != null;
		}

		/// <summary>
		/// Returns all roles of the user
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		public string[] GetRoles(string userName)
		{			
			IQueryable<User> users = Entities.Users.Include(u => u.Roles);
			User user = users.Include(u=>u.Groups).FirstOrDefault<User>(u => u.UserName == userName);
			
			IEnumerable<String> roles = from r in user.Roles
						select r.Name;

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
		}
	}
}