using MonackFr.Mvc.Areas.PackageManagement.Contexts;
using MonackFr.Mvc.Areas.PackageManagement.Repositories;
using MonackFr.Repository;
using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Security;

namespace MonackFr.Mvc.Areas.PackageManagement.Controllers
{
	[Export(typeof(IModule))]
	public class PackageController : DisposeController, IModule, IAuthorization
	{
		private enum PackageControllerRoles
		{
			[Description("Install Packages")]
			InstallPackage
		}

		private IPackageRepository _packageRepository;
				
		private List<Package> _getPackages()
		{
			//get the folder that's in
			string dir = AppDomain.CurrentDomain.BaseDirectory;
			string[] files = Directory.GetFiles(string.Format("{0}{1}\\", dir, Application.PackageDir), "*.dll", SearchOption.AllDirectories);
			List<Package> packages = new List<Package>();

			foreach (string file in files)
			{
				Package package = new Package(file);
				package.LoadModules();
				if (package.Modules != null && package.Modules.Count() > 0)
				{
					package.Installed = (_packageRepository.GetSingle(x => x.Path == package.Path) != null);
					packages.Add(package);
				}
			}
			
			return packages;
		}

		public PackageController()
			: base()
		{
			_packageRepository = new PackageRepository();
			this.AddDisposable((IDisposable)_packageRepository);
		}

		public PackageController(IPackageRepository repository)			
			: base((IDisposable)repository)
		{
		}	

		[Role(PackageControllerRoles.InstallPackage)]
		public ActionResult Index()
		{
			List<Package> modules = _getPackages();
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

		public List<MfrRole> GetRoles()
		{
			List<MfrRole> roles = new List<MfrRole>();

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
