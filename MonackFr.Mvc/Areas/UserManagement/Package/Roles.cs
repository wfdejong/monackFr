using MonackFr.Security;

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