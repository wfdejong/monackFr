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
		/// Packages to install
		/// </summary>
		private IPackageManager _packageManager = null;

		#region constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public InstallController()
		{			
            _packageManager = new PackageManager();
            _packageManager.BaseDirectory = string.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
            _packageManager.PackageDirectory = string.Format("{0}{1}\\", _packageManager.BaseDirectory, ApplicationSettings.PackageDir);			
		}

		/// <summary>
		/// Constructor for testing purposes
		/// </summary>
		/// <param name="directory"></param>
		public InstallController(IPackageManager packageManager)
		{
			_packageManager = packageManager;
		}

		#endregion

		/// <summary>
		/// Show install page
		/// </summary>
		/// <returns></returns>
		public ActionResult Install()
		{
			PackageList packageList = new PackageList();
            _packageManager.LoadPackages();

            packageList.Packages = _packageManager.Packages;

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
				
				_packageManager.LoadPackages();
                
				foreach (IPackage package in _packageManager.Packages)
				{
					context.Contexts.AddRange(package.Contexts);
				}

				Database.SetInitializer(new DropCreateDatabaseAlways<Context>());
				context.Database.Initialize(true);

				InstallRoles();
                InstallPackages(_packageManager.Packages);

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

            _packageManager.LoadPackages();

			foreach (IPackage package in _packageManager.Packages)
			{
				Loader<IAuthorization> loader = new Loader<IAuthorization>(baseDir + package.Path);
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
		private void InstallPackages(IPackage[] packages)
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
