using System.Web;
using System.Web.Mvc;

namespace MonackFr.Mvc.Areas.UserManagement
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}