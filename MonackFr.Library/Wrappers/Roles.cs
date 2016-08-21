using MonackFr.Security;

namespace MonackFr.Wrappers
{
	public class Roles : IRoles
	{
		void IRoles.CreateRole(IMfrRole role)
		{
			if (System.Web.Security.Roles.Provider is IMfrRoleProvider)
			{
				IMfrRoleProvider mfrRoleProvider = (IMfrRoleProvider)System.Web.Security.Roles.Provider;
				mfrRoleProvider.CreateMfrRole(role);
			}
			else
			{
				System.Web.Security.Roles.Provider.CreateRole(role.Name);
			}
		}
	}
}
