using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Optimization;
using MonackFr.Library.Module;
using MonackFr.Mvc.Repositories;

namespace MonackFr.Mvc
{
    public class Global : HttpApplication
    {
        /// <summary>
        /// Code that runs on application startup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Start(object sender, EventArgs e)
        {            
            LoadInstalledModules();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.CreateMaps();
        }

        /// <summary>
		/// Loads all installed modules
		/// </summary>
		private void LoadInstalledModules()
        {
            try
            {

                //Since pluginloader is singleton, the plugins are present if loaded once.
                if (ModuleKeeper.Instance.Modules.Count() == 0)
                {
                    PackageRepository packageRepository = new PackageRepository();
                    IEnumerable<Entities.Package> packages = packageRepository.GetAll().ToList();

                    foreach (Entities.Package package in packages)
                    {
                        string path = string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory,
                            package.Path);
                        ILoader<IModule> loader = new Loader<IModule>();
                        IEnumerable<IModule> loadedModules = loader.Load(path).LoadedItems;
                        ModuleKeeper.Instance.AddRange(loadedModules);
                    }
                }
            }
            catch (EntityException ex)
            {
                
            }
        }
    }
}