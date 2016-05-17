using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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
            AutoMapperConfig.CreateMaps();
			
			context.MapRoute(
				"UserManagement_default",
				"users/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}