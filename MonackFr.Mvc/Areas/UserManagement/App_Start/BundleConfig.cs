using System.Web.Optimization;

namespace MonackFr.Mvc.Areas.UserManagement
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/usermanagement").Include(
                "~/Areas/UserManagement/Scripts/users-controller.js",
                "~/Areas/UserManagement/Scripts/users-api.js",
                "~/Areas/UserManagement/Scripts/roles-controller.js",
                "~/Areas/UserManagement/Scripts/roles-api.js",
                "~/Areas/UserManagement/Scripts/groups-controller.js",
                "~/Areas/UserManagement/Scripts/groups-api.js"
                ));

            BundleTable.EnableOptimizations = false;
        }
    }
}