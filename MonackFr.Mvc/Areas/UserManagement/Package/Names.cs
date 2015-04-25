using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.UserManagement.Package
{
	internal static class Names
	{
		internal static class Menu
		{
			internal static class Users
			{
				internal const string Index = "MonackFr.UserManagement.Users.Index";
				internal const string Details = "MonackFr.UserManagement.Users.UserDetails";
			}

			internal static class Groups
			{
				internal const string Index = "MonackFr.UserManagement.Group.Index";
			}
		}

		internal static class Panel
		{
			internal class Users
			{
				internal const string Index = "MonackFr.UserManagerment.Users.Panel.Index";
				internal const string Details = "MonackFr.UserManagement.Users.Panel.UserDetails";
			}

			internal class Groups
			{
				internal const string Index = "MonackFr.UserManagerment.Group.Panel.Index";
			}
		}
	}
}