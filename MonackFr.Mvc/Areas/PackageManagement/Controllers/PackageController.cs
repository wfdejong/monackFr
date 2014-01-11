using MonackFr.Mvc.Areas.PackageManagement.Repositories;
using MonackFr.Mvc.Areas.PackageManagement.Entities;
using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using MonackFr.Mvc.Module;

namespace MonackFr.Mvc.Areas.PackageManagement.Controllers
{
	[Export(typeof(IModule))]
	[Export(typeof(IAuthorization))]
	public class PackageController : DisposeController, IModule, IAuthorization
	{
		private enum PackageControllerRoles
		{
			[Description("Install Packages")]
			InstallPackage
		}

		private IPackageRepository _packageRepository;
		private IPackageManager _packageManager;

		public PackageController()
			: base()
		{
			_packageRepository = new PackageRepository();
			this.AddDisposable((IDisposable)_packageRepository);

			_packageManager = new PackageManager();

            _packageManager.BaseDirectory = string.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
            _packageManager.PackageDirectory = string.Format("{0}{1}\\", _packageManager.BaseDirectory, ApplicationSettings.PackageDir);            
		}

		public PackageController(IPackageRepository packageRepository, IPackageManager packageManager)			
			: base((IDisposable)packageRepository)
		{
            _packageRepository = packageRepository;
            _packageManager = packageManager;
		}	

		[Role(PackageControllerRoles.InstallPackage)]
		public ActionResult Index()
		{
			
			_packageManager.LoadPackages();
			IEnumerable<IPackage> modules = _packageManager.Packages;

			IEnumerable<Package> installedPackages = _packageRepository.GetAll();
			IEnumerable<string> installedPackagesPaths = from p in installedPackages select p.Path;

			(from m in modules select m).ToList().ForEach((module) =>
			{
				module.Installed = installedPackagesPaths.Contains(module.Path);					
			});

			return View(modules);
		}

		[HttpPost]
		[Role(PackageControllerRoles.InstallPackage)]
		public ActionResult InstallPackage(string path)
		{
			Package package = new Package(path);
			_packageRepository.Create(package);
			_packageRepository.Save();			
			return RedirectToAction("index");
		}

		[HttpPost]
		[Role(PackageControllerRoles.InstallPackage)]
		public ActionResult RemovePackage(string path)
		{
			Package package = _packageRepository.GetSingle(x => x.Path == path);
			_packageRepository.Delete(package);
			_packageRepository.Save();
			return RedirectToAction("index");
		}

		#region implementation of IModule
		
		MenuItem IModule.GetMenu()
		{
			MenuItem menuItem = new MenuItem();
			menuItem.Text = "Packages";

			menuItem.MenuItems = new List<MenuItem>();
			menuItem.MenuItems.Add(new MenuItem { Text = "Packages", ActionName = "Index", Controller = "Package", Area = "PackageManegement" });

			return menuItem;			
		}

		Dictionary<string, string> IModule.MetaData
		{
			get
			{
				Dictionary<string, string> metaData = new Dictionary<string, string>();
				metaData.Add("Name", "PackageManager");
                metaData.Add("Author", "Willem de Jong");

				return metaData;
			}
		}
		
		#endregion //implementation of IModule

		#region of IAuthorization

		public List<IMfrRole> GetRoles()
		{
			List<IMfrRole> roles = new List<IMfrRole>();

			foreach (PackageControllerRoles role in Enum.GetValues(typeof(PackageControllerRoles)))
			{
				roles.Add(new MfrRole()
				{
					Name = role.ToString(),
					Description = role.ToDescription()
				});
			}

			return roles;
		}

		#endregion //implementation of IAuthorization

	}
}
