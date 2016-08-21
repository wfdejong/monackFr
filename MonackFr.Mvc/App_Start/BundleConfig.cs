using System.Web.Optimization;

namespace MonackFr.Mvc
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/angular").Include(
                "~/Scripts/Libraries/angular.js",
                "~/Scripts/Libraries/angular-resource.js",
                "~/Scripts/Libraries/angular-ui-router.js"));

            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                "~/scripts/libraries/jquery-2.2.4.js"));

            bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include(
                "~/Scripts/Libraries/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/scripts/monackfr").Include(
                "~/Scripts/monackfr.js",
                "~/Scripts/Tiles/tile-controller.js",
                "~/Scripts/Tiles/tile-api.js"));

            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                "~/Css/bootstrap.css",
                "~/Css/bootstrap-theme.css"));

            bundles.Add(new ScriptBundle("~/css/monackfr").Include(
                "~/Css/monackfr.css"));
        }
    }
}