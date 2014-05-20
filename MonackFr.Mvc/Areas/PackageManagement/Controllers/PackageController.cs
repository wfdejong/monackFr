using MonackFr.Mvc.Repositories;
using MonackFr.Mvc.Areas.PackageManagement.Entities;
using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using MonackFr.Wrappers;
using MonackFr.Module;

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
        private IFile _file;

		public PackageController()
			: base()
		{
			_packageRepository = new PackageRepository();
			this.AddDisposable((IDisposable)_packageRepository);

			_packageManager = new PackageManager();
            _file = new Wrappers.File();

            _packageManager.BaseDirectory = string.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
            _packageManager.PackageDirectory = string.Format("{0}{1}\\", _packageManager.BaseDirectory, ApplicationSettings.PackageDir);            
		}

		public PackageController(IPackageRepository packageRepository, IPackageManager packageManager, IFile file)			
			: base((IDisposable)packageRepository)
		{
            _packageRepository = packageRepository;
            _packageManager = packageManager;
            _file = file;
		}	

		[Role(PackageControllerRoles.InstallPackage)]
		public ActionResult Index()
        {
            return null;		
            //_packageManager.LoadPackages();
            //IEnumerable<IPackage> modules = _packageManager.Packages;

            //IEnumerable<Package> installedPackages = _packageRepository.GetAll();
            //IEnumerable<string> installedPackagesPaths = from p in installedPackages select p.Path;

            //(from m in modules select m).ToList().ForEach((module) =>
            //{
            //    module.Installed = installedPackagesPaths.Contains(module.Path);					
            //});

            //return View(modules);
		}

		[HttpPost]
		[Role(PackageControllerRoles.InstallPackage)]
		public ActionResult InstallPackage(string path)
		{
            //if (!_file.Exists(path))
            //{
            //    throw new FileNotFoundException(path);
            //}

            //Package package = new Package(path);
            //_packageRepository.Create(package);
            //_packageRepository.Save();
            //return RedirectToAction("index");
            return null;
		}

		[HttpPost]
		[Role(PackageControllerRoles.InstallPackage)]
		public ActionResult DeletePackage(string path)
		{
            //if (!_file.exists(path))
            //{
            //    throw new filenotfoundexception(path);
            //}

            //package package = _packagerepository.getsingle(x => x.path == path);
            //_packagerepository.delete(package);
            //_packagerepository.save();
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

		Tile IModule.GetTile(UrlHelper url)
		{

			Tile tile = new Tile();
			tile.Title = "Package management";
			tile.Url = url.Action("Index", "Package", new { area = "PackageManagement" });
			tile.Copyright = "The Monack Framework";

			return tile;
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
