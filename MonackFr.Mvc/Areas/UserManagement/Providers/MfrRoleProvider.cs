using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MonackFr.Security;
using MonackFr.Mvc;
using MonackFr.Mvc.Entities;
using MonackFr.Mvc.Repositories;

namespace MonackFr.Mvc.Areas.UserManagement.Providers
{
	public class MfrRoleProvider : RoleProvider, IMfrRoleProvider
	{
		private IRoleRepository _repository;

		public MfrRoleProvider()
		{
			_repository = new RoleRepository();
		}

		public MfrRoleProvider(IRoleRepository repository)
		{
			_repository = repository;
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			_repository.AddUsersToRoles(usernames, roleNames);
			_repository.Save();
		}

		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override void CreateRole(string roleName)
		{
			if (!RoleExists(roleName))
			{
				Role role = new Role { Name = roleName };
				_repository.Create(role);
				_repository.Save();
			}
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			string[] roles =  _repository.GetAll().Select(r => r.Name).ToArray();
			return roles;
		}

		public override string[] GetRolesForUser(string userName)
		{
			IUserRepository repository = new UserRepository();
			return repository.GetRoles(userName);
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool IsUserInRole(string userName, string roleName)
		{
			throw new NotImplementedException();
			/*var user = _repository.GetUser(username);

			return string.Compare(user.Role.Name, roleName, true) == 0;*/
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			_repository.RemoveUsersFromRoles(usernames, roleNames);
			_repository.Save();
		}

		public override bool RoleExists(string roleName)
		{
			return (_repository.GetSingle(r => r.Name == roleName) != null);
		}

		void IMfrRoleProvider.CreateMfrRole(IMfrRole mfrRole)
		{
			if (!RoleExists(mfrRole.Name))
			{
				Role role = new Role(mfrRole);
				_repository.Create(role);
				_repository.Save();
			}
		}
	}
}