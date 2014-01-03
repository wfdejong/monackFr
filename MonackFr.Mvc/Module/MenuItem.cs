using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Security;
using System.Web.Security;

namespace MonackFr.Mvc.Module
{
	/// <summary>
	/// Menu item
	/// </summary>
	public class MenuItem
	{
		/// <summary>
		/// TODO: refactor to label
		/// Text of the menu item
		/// </summary>
		public String Text { get; set; }
		
		/// <summary>
		/// Name of action
		/// </summary>
		public String ActionName { get; set; }

		/// <summary>
		/// Controller of action
		/// </summary>
		public String Controller { get; set; }

		/// <summary>
		/// Area of action
		/// </summary>
		public String Area { get; set; }

		/// <summary>
		/// Nested menu items
		/// </summary>
		public List<MenuItem> MenuItems { get; set; }

		/// <summary>
		/// Roles that have access to action
		/// </summary>
		public string[] UserRoles { get; set; }

		/// <summary>
		/// Checks if user has user has role 
		/// </summary>
		/// <param name="user"></param>
		/// <returns>true if user has role or if roles is null/empty</returns>
		public bool Authorized(MfrUser user)
		{
			if (user != null)
			{
				if (UserRoles == null || UserRoles.Count() == 0)
				{
					return true;
				}

				for (int i = 0; i < UserRoles.Length; i++)
				{
					//TODO: check if check hits the db, should be once and check against array
					if (Roles.IsUserInRole(user.UserName, UserRoles[i]))
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}