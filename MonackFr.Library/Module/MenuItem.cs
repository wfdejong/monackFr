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
		public string Label { get; set; }

		/// <summary>
		/// Url to view
		/// </summary>
		public string Url { get; set; }
		
		/// <summary>
		/// The system name used internally. Should be unique in the whole application
		/// </summary>
		public string SystemName { get; set; }

		/// <summary>
		/// System name converted into css compatible text
		/// </summary>
		public string CssSystemName
		{
			get
			{
				return Format.ToCss(SystemName);
			}
		}

		/// <summary>
		/// True if this menu item should be activated on default. 
		/// This will show the corresponding panel.
		/// </summary>
		public bool Default { get; set; }

		/// <summary>
		/// Nested menu items
		/// </summary>
		public IEnumerable<MenuItem> MenuItems { get; set; }

		/// <summary>
		/// Roles that have access to action
		/// </summary>
		public IEnumerable<string> UserRoles { get; set; }
		
		/// <summary>
		/// Constructor to maken Label and System name required.
		/// </summary>
		/// <param name="Label"></param>
		/// <param name="SystemName"></param>
		public MenuItem(string label, string systemName)
		{
			SystemName = systemName;
			Label = label;
		}
	}
}