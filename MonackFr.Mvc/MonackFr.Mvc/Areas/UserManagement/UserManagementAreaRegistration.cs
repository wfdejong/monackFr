using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonackFr.Mvc.Areas.UserManagement
{
	public class UserManagementAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "UserManagement";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"UserManagement_default",
				"users/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}