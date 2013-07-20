using System.Web.Mvc;

namespace MonackFr.Mvc.Areas.TaskListArea
{
	public class DemoProjectAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "DemoProject";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"DemoProject_default",
				"DemoProject/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
