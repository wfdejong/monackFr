using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.UserManagement.Package
{
	internal enum UserControllerRoles
	{
		[Description("View Users")]
		ViewUser,

		[Description("Edit users")]
		EditUser,

		[Description("Create users")]
		CreateUser,

		[Description("Delete users")]
		DeleteUser
	};
}