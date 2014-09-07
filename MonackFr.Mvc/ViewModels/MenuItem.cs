using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.ViewModels
{
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
		/// Roles that have access to action
		/// </summary>
		public IEnumerable<string> UserRoles { get; set; }
	}
}