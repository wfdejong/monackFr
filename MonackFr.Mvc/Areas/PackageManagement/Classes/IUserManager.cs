using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.PackageManagement
{
	/// <summary>
	/// Handles user functionality
	/// </summary>
    public interface IUserManager
    {
		/// <summary>
		/// Creates a user
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		void CreateUser(string username, string password);

		/// <summary>
		/// Adds user to all existing roles
		/// </summary>
		/// <param name="username"></param>
		void AddUserToAllRoles(string username);
    }
}