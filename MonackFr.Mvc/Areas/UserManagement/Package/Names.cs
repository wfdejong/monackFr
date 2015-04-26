using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.UserManagement.Package
{
	public static class Names
	{
		public static class Menus
		{
			public static class Users
			{
				public const string Index = "MonackFr.UserManagement.Users.Index";
				public const string Details = "MonackFr.UserManagement.Users.UserDetails";
			}

			public static class Groups
			{
				public const string Index = "MonackFr.UserManagement.Group.Index";
			}
		}

		public static class Panels
		{
			public class Users
			{
				public const string Index = "MonackFr.UserManagerment.Users.Panel.Index";
				public const string Details = "MonackFr.UserManagement.Users.Panel.UserDetails";
			}

			public class Groups
			{
				public const string Index = "MonackFr.UserManagerment.Group.Panel.Index";
			}
		}
	}
}