using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MonackFr.Mvc.Areas.PackageManagement
{
	/// <summary>
	/// Handles user functionality
	/// </summary>
    public class UserManager : IUserManager
    {
		/// <summary>
		/// Creates a user
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
        void IUserManager.CreateUser(string username, string password)
        {
            Membership.CreateUser(username, password);
        }

		/// <summary>
		/// Adds user to all existing roles
		/// </summary>
		/// <param name="username"></param>
        void IUserManager.AddUserToAllRoles(string username)
        {
            string[] roles = Roles.GetAllRoles();

            if (roles.Length > 0)
            {
                Roles.AddUserToRoles("admin", roles);
            }
        }
    }
}