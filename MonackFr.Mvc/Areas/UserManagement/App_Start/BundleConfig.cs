using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;
using System.Web;

namespace MonackFr.Mvc.Areas.UserManagement
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
#if DEBUG
            bundles.Add(new ScriptBundle("~/areas/scripts/user").Include(
                "~/Areas/UserManagement/Scripts/User.js"));

#else
#endif

        }
    }
}