using MonackFr.Mvc.Areas.Install.ViewModels;
using MonackFr.Mvc.Areas.PackageManagement.Repositories;
using MonackFr.Repository;
using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MonackFr.Mvc.Areas.Install.Controllers
{
	/// <summary>
	/// This controller is used for installing the framework. It creates the database and
	/// the necesary tables.
	/// </summary>
    public class InstallController : Controller
    {
		/// <summary>
		/// Returns dll files from the plugin directory
		/// </summary>
		private string[] _pluginFiles
		{
			get
			{
				string baseDir = string.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
				string moduleDir = string.Format("{0}{1}\\", baseDir, ApplicationSettings.PackageDir);

				string[] files = Directory.GetFiles(moduleDir, "*.dll", SearchOption.AllDirectories);

				for (int i = 0; i < files.Count(); i++)
				{
					files[i] = files[i].Substring(baseDir.Count());
				}

				return files;
			}
		}
		
		/// <summary>
		/// Scans the plugin directory for packages and returns them.
		/// </summary>
		/// <returns></returns>
		private IEnumerable<Package> _getPackages()
		{
			List<Package> packages = new List<Package>();

			foreach (string file in _pluginFiles)
			{
				Package package = new Package(file);
				package.LoadContexts();
				package.LoadModules();
				if (package.Contexts.Count() > 0 && package.Modules.Count() > 0)
				{
					packages.Add(package);
				}
			}

			return packages;
		}

		/// <summary>
		/// Show install page
		/// </summary>
		/// <returns></returns>
		public ActionResult Install()
		{
			PackageList packageList = new PackageList();
			packageList.Packages = _getPackages();

			return View(packageList);
		}

		/// <summary>
		/// Do installation
		/// </summary>
		/// <param name="modules"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Install(FormCollection modules)
		{
			using (Context context = new Context())
			{
				
				//TODO: make this more efficient by using one foreach loop
				context.Contexts = new List<IContext>();
				
				IEnumerable<Package> packages = _getPackages();
				foreach (Package package in packages)
				{
					context.Contexts.AddRange(package.Contexts);
				}

				Database.SetInitializer(new DropCreateDatabaseAlways<Context>());
				context.Database.Initialize(true);

				InstallRoles();
				InstallPackages(packages);

				context.SaveChanges();

				Membership.CreateUser("admin", "admin");
				Roles.AddUserToRoles("admin", Roles.GetAllRoles());

				return RedirectToAction("Install");
			}

		}

		/// <summary>
		/// Install all roles defined in the packages
		/// </summary>
		private void InstallRoles()
		{
			//TODO: convert to function that use packages
			string baseDir = string.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
			
			List<IAuthorization> authorizations = new List<IAuthorization>();

            foreach (string file in _pluginFiles)
            {
                Loader<IAuthorization> loader = new Loader<IAuthorization>(baseDir + file);
                authorizations.AddRange(loader.LoadedItems);
            }
			foreach (IAuthorization authorization in authorizations)
			{
				IEnumerable<MfrRole> roles = authorization.GetRoles();

				if (Roles.Provider is IMfrRoleProvider)
				{
					IMfrRoleProvider mfrRoleProvider = (IMfrRoleProvider)Roles.Provider;

					foreach (MfrRole role in roles)
					{
						mfrRoleProvider.CreateMfrRole(role);
					}
				}
				else
				{
					foreach (MfrRole role in roles)
					{
						Roles.CreateRole(role.Name);
					}
				}
			}			
		}

		/// <summary>
		/// Installs the packages in the database.
		/// </summary>
		/// <param name="packages"></param>
		private void InstallPackages(IEnumerable<Package> packages)
		{
			PackageRepository packageRepository = new PackageRepository();
			foreach (Package package in packages)
			{
				packageRepository.Create(package);
			}
			packageRepository.Save();
			packageRepository.Dispose();
		}
    }
}
