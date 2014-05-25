using MonackFr.Mvc.Repositories;
using MonackFr.Mvc.Areas.PackageManagement.ViewModels;
using MonackFr.Mvc.Areas.PackageManagement.Entities;
using MonackFr.Repository;
using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using MonackFr.Module;

namespace MonackFr.Mvc.Areas.PackageManagement.Controllers
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
        private IPackageRepository _packageRepository = null;
        private IDatabaseManager _databaseManager = null;
        private IUserManager _userManager = null;

		#region constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public InstallController()
		{			
            _packageManager = new PackageManager();
            _packageManager.BaseDirectory = string.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
            _packageManager.PackageDirectory = string.Format("{0}{1}\\", _packageManager.BaseDirectory, ApplicationSettings.PackageDir);
            
            _packageRepository = new PackageRepository();
            _databaseManager = new DatabaseManager();
           _userManager = new UserManager();
		}

		/// <summary>
		/// Constructor for testing purposes
		/// </summary>
		/// <param name="directory"></param>
		public InstallController(IPackageManager packageManager, IPackageRepository packageRepository, IDatabaseManager databaseManager/*, IUserManager userManager*/)
		{
			_packageManager = packageManager;
            _packageRepository = packageRepository;
            _databaseManager = databaseManager;
            //_userManager = userManager;
		}

		#endregion

		/// <summary>
		/// Show install page
		/// </summary>
		/// <returns></returns>
		public ViewResult Install()
		{
			PackageList packageList = new PackageList();
            _packageManager.LoadPackages();

            packageList.Packages = Mapper.Map<IEnumerable<ViewModels.Package>>(_packageManager.Packages);

			return View("install", packageList);
		}

		/// <summary>
		/// Install packages
		/// </summary>
		/// <param name="modules"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Install(FormCollection packages)
		{

            //TODO: install only selected modules

			using (Context context2 = new Context())
			{		
				_packageManager.LoadPackages();
                
                //install database
                _databaseManager.InstallDatabase(_packageManager.Contexts);
                
				_packageManager.InstallRoles();
                IEnumerable<Mvc.Entities.Package> ps = Mapper.Map<IEnumerable<Mvc.Entities.Package>>(_packageManager.Packages);
                
                _packageRepository.InstallPackages(ps);
                _packageRepository.Dispose();
				context2.SaveChanges();

                _userManager.CreateUser("admin", "admin");
                _userManager.AddUserToAllRoles("admin");
				
				return RedirectToAction("Install");
			}

		}
	}
}
