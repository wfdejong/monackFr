using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using MonackFr.Repository;
using MonackFr.Mvc.App_Start;
using MonackFr.Mvc.Repositories;
using MonackFr.Module;
using System.Web.Configuration;

namespace MonackFr.Mvc
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			if (ApplicationSettings.DbInstalled)
			{
				LoadInstalledModules();
			}

            AutoMapperConfig.CreateMaps();
			AreaRegistration.RegisterAllAreas();

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
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
						string path = string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, package.RelativePath);
						IEnumerable<IModule> loadedModules = new Loader<IModule>(path).LoadedItems;
						ModuleKeeper.Instance.AddRange(loadedModules);
					}
				}
			}
			catch(Exception ex)
			{
				//database is not installed or no access to database				
			}
		}
	}
}
