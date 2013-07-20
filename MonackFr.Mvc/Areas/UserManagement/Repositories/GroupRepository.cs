using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Repository;
using MonackFr.Mvc.Areas.UserManagement.Models;

namespace MonackFr.Mvc.Areas.UserManagement.Repositories
{
	public class GroupRepository : GenericRepository<UserManagementContext, Group>, IGroupRepository 
	{
		public void AddRolesToGroup(Group group, Role[] roles)
		{
			for (int i = 0; i < roles.Length; i++)
			{
				int roleId = roles[i].Id; //Linq to entities can't handle array references
				Role role = Entities.Roles.FirstOrDefault<Role>(r => r.Id == roleId);
				group.Roles.Add(role);
				this.Save();
			}
		}

		public void RemoveAllRolesFromGroup(Group group)
		{
			Role[] roles = group.Roles.ToArray<Role>();

			for (int i = 0; i < roles.Length; i++)
			{
				Role role = roles[i];
				group.Roles.Remove(role);
			}

			this.Save();
		}

		public void AddUserToGroup(int groupId, int userId)
		{
			//TODO: use only id's to connect
			Group group = Entities.Groups.FirstOrDefault(g => g.Id == groupId);
			User user = Entities.Users.FirstOrDefault(u => u.Id == userId);
			group.Users.Add(user);
			this.Save();
		}

		public void RemoveUserFromGroup(int groupId, int userId)
		{
			//create dummy objects 
			Group dummyGroup = new Group { Id = groupId };
			User dummyUser = new User { Id = userId };
			dummyGroup.Users = new List<User> {dummyUser};

			//start tracking and remove
			Entities.Groups.Attach(dummyGroup);
			dummyGroup.Users.Remove(dummyUser);
			this.Save();

		}
	}
}