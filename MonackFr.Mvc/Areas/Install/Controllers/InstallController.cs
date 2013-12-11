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
		/// Wrapper for System.IO.Directory
		/// </summary>
		private MonackFr.Wrappers.IDirectory _directory = null;

		/// <summary>
		/// Path to module directory
		/// </summary>
		string _moduleDirectory;

		/// <summary>
		/// Path to root of application
		/// </summary>
		string _baseDirectory = null;

		/// <summary>
		/// Packages to install
		/// </summary>
		private IPackageManager _pakageManager = null;

		#region private methods
				
		/// <summary>
		/// Scans the plugin directory for packages and returns them.
		/// </summary>
		/// <returns></returns>
		private IPackage[] _getPackages()
		{
			string[] files = _directory.GetFiles(_moduleDirectory, "*.dll", SearchOption.AllDirectories);

			foreach (string file in files)
			{
				string relativePath = file.Substring(_baseDirectory.Count());
				Package package = new Package(relativePath);
				_pakageManager.AddPackage(package);				
			}

            return _pakageManager.Packages;
		}

		#endregion //private methods

		#region constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public InstallController()
		{			
            _pakageManager = new PackageManager();
			_baseDirectory = string.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
			_moduleDirectory = string.Format("{0}{1}\\", _baseDirectory, ApplicationSettings.PackageDir);
		}

		/// <summary>
		/// Constructor for testing purposes
		/// </summary>
		/// <param name="directory"></param>
		public InstallController(MonackFr.Wrappers.IDirectory directory, string baseDirectory, string moduleDirectory, IPackageManager packageManager)
		{
			_directory = directory;
			_pakageManager = packageManager;
			_baseDirectory = baseDirectory;
			_moduleDirectory = moduleDirectory;
		}

		#endregion

		/// <summary>
		/// Show install page
		/// </summary>
		/// <returns></returns>
		public ActionResult Install()
		{
			PackageList packageList = new PackageList();
            _pakageManager.LoadPackages(_moduleDirectory, _baseDirectory);

            packageList.Packages = _pakageManager.Packages;

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
				
				IPackage[] packages = _getPackages();
				foreach (IPackage package in packages)
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

			foreach (Package package in _getPackages())
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
