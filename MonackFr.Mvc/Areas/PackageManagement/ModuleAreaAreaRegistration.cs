using System.Web.Mvc;

namespace MonackFr.Mvc.Areas.PackageManagement
{
	public class PackageManagementAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "PackageManagement";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
            AutoMapperConfig.CreateMaps();

			context.MapRoute(
				"PackageManagement_default",
				"PackageManagement/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
