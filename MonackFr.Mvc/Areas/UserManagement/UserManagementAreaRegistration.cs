using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

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
            RegisterRoutes(context);
            RegisterBundles();
        }

        private void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapRoute(
               "UserManagement_default",
               "usermanagement/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional }
           );
        }

        private void RegisterBundles()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}