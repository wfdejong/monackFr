using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Security;
using System.Web.Security;

namespace MonackFr.Module
{
	/// <summary>
	/// Menu item
	/// </summary>
	public class MenuItem
	{
		/// <summary>
		/// Text of the menu item
		/// </summary>
		public String Label { get; set; }
		
		/// <summary>
		/// Name of action
		/// </summary>
		public String Action { get; set; }

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
		public IEnumerable<string> UserRoles { get; set; }

		/// <summary>
		/// Checks if user has user has role 
		/// </summary>
		/// <param name="user"></param>
		/// <returns>true if user has role or if roles is null/empty</returns>
		[Obsolete("Should be refactored and handled somewhere else", true)]
		public bool Authorized(MfrUser user)
		{
			if (user != null)
			{
				if (UserRoles == null || UserRoles.Count() == 0)
				{
					return true;
				}

				foreach(string userRole in UserRoles)
				{
					//TODO: check if check hits the db, should be once and check against array
					if (Roles.IsUserInRole(user.UserName, userRole))
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}