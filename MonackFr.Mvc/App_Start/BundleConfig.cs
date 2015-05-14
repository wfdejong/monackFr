using System.Web;
using System.Web.Optimization;

namespace MonackFr.Mvc
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-2.*"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
						"~/Scripts/jquery-ui*"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.unobtrusive*",
						"~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

			bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
						"~/Content/themes/base/jquery.ui.core.css",
						"~/Content/themes/base/jquery.ui.resizable.css",
						"~/Content/themes/base/jquery.ui.selectable.css",
						"~/Content/themes/base/jquery.ui.accordion.css",
						"~/Content/themes/base/jquery.ui.autocomplete.css",
						"~/Content/themes/base/jquery.ui.button.css",
						"~/Content/themes/base/jquery.ui.dialog.css",
						"~/Content/themes/base/jquery.ui.slider.css",
						"~/Content/themes/base/jquery.ui.tabs.css",
						"~/Content/themes/base/jquery.ui.datepicker.css",
						"~/Content/themes/base/jquery.ui.progressbar.css",
						"~/Content/themes/base/jquery.ui.theme.css"));
			
			bundles.Add(new ScriptBundle("~/bundles/tiles").Include(
						"~/Scripts/tiles.jquery.js"));

			bundles.Add(new ScriptBundle("~/bundles/datatablesjs").Include(
						"~/Scripts/jquery.dataTables.js",
						"~/Scripts/dataTables.tableTools.min.js"));
			
			bundles.Add(new ScriptBundle("~/bundles/datatablesjsui").Include(
						"~/Script/dataTables.jqueryui.js"));

			

			bundles.Add(new ScriptBundle("~/content/themes/base/datatablescss").Include(
						"~/Content/themes/base/jquery.dataTables.css",
						"~/Content/themes/base/jquery.dataTables_themeroller.css",
						"~/Content/themes/base/dataTables.jqueryui.css,",
						"~/Content/themes/base/dataTables.tableTools.css"));	
		}
	}
}