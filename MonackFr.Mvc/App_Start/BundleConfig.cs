using System.Web;
using System.Web.Optimization;

namespace MonackFr.Mvc
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
#if DEBUG
            bundles.Add(new ScriptBundle("~/scripts/angular").Include(
                "~/Scripts/Libraries/angular.js",
                "~/Scripts/Libraries/angular-resource.js",
                "~/Scripts/Libraries/angular-ui-router.js"));

            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                "~/scripts/libraries/jquery-2.2.4.js"));

            bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include(
                "~/Scripts/Libraries/bootstrap.js"));

            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                "~/Css/bootstrap.css",
                "~/Css/bootstrap-theme.css"));

            bundles.Add(new ScriptBundle("~/css/monackfr").Include(
                "~/Css/monackfr.css"));

#else
    //TODO:use minified version here
            bundles.Add(new ScriptBundle("~/scripts/angular").Include(
                        "~/Scripts/Libraries/angular.js",
                        "~/Scripts/Libraries/angular-resource.js",
                        "~/Scripts/Libraries/angular-ui-router.js"));
            
            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                "~/scripts/libraries/jquery-2.2.4.min.js"));

            
            bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include(
                "~/Scripts/Libraries/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                "~/Css/bootstrap.min.css",
                "~/Css/bootstrap-theme.min.css"));

            bundles.Add(new ScriptBundle("~/css/monackfr").Include(
                "~/Css/monackfr.css"));
#endif
        }
    }
}