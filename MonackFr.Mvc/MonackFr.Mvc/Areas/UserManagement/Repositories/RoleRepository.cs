﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Mvc.Areas.UserManagement.Models;
using MonackFr.Repository;

namespace MonackFr.Mvc.Areas.UserManagement.Repositories
{
	public class RoleRepository : GenericRepository<UserManagementContext, Role>, IRoleRepository
	{
		public void AddUsersToRoles(String[] userNames, String[] roleNames)
		{
			//make a list of users first so it can be reused
			List<User> users = new List<User>();
			
			foreach (String userName in userNames)
			{
				users.Add(this.Entities.Users.FirstOrDefault<User>(u => u.UserName == userName));
			}			

			//add users to each role
			foreach (String roleName in roleNames)
			{
				Role role = this.GetSingle(r => r.Name == roleName);
				this.Entities.Roles.Include("Users");
				foreach (User user in users)
				{
					if (role.Users == null)
					{
						role.Users = new List<User>();
					}
					role.Users.Add(user);
				}
				this.Edit(role);
			}
		}

		public void RemoveUsersFromRoles(String[] userNames, String[] roleNames)
		{
			//TODO: make function without loops: 1 query should be sufficient.
			//make a list of users first so it can be reused
			List<User> users = new List<User>();

			foreach (String userName in userNames)
			{
				users.Add(this.Entities.Users.FirstOrDefault<User>(u => u.UserName == userName));
			}

			//add users to each role
			foreach (String roleName in roleNames)
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