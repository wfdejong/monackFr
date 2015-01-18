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
using MonackFr.Repository;
using MonackFr.Mvc.Contexts;

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
		private IMappingEngine _mapper = null;

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
			_mapper = Mapper.Engine;
		}

		/// <summary>
		/// Constructor for testing purposes
		/// </summary>
		/// <param name="directory"></param>
		public InstallController(IPackageManager packageManager, IPackageRepository packageRepository, IDatabaseManager databaseManager, IUserManager userManager, IMappingEngine mapper)
		{
			_packageManager = packageManager;
            _packageRepository = packageRepository;
            _databaseManager = databaseManager;
			_userManager = userManager;
			_mapper = mapper;
		}

		#endregion

		/// <summary>
		/// Show install page
		/// </summary>
		/// <returns></returns>
		public ViewResult Install()
		{
			PackageList packageList = new PackageList();
            IEnumerable<Package> packages = _packageManager.GetPackages();

            packageList.Packages = _mapper.Map<IEnumerable<ViewModels.Package>>(packages);

			return View("install", packageList);
		}

		/// <summary>
		/// Install packages
		/// </summary>
		/// <param name="modules"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Install(FormCollection packages_)
		{

            //TODO: install only selected packages

			using (Context context2 = new Context())
			{		
				IEnumerable<Package> packages = _packageManager.GetPackages();
                List<IContext> contexts = new List<IContext>();
                //List<IAuthorization> authorizations = new List<IAuthorization>();

                //Add system contexts
                contexts.Add(new PackageContext());
                contexts.Add(new UserManagementContext());

                foreach (Package package in packages)
                {
                    contexts.AddRange(package.Contexts);                    
                }

                
                //install database
                _databaseManager.InstallDatabase(contexts);
                
                //save roles to database
                //TODO: should go with relation of package->module-roles. 
				//_packageManager.InstallRoles(authorizations);

                //save packages
                IEnumerable<Mvc.Entities.Package> entityPackages = _mapper.Map<IEnumerable<Mvc.Entities.Package>>(packages);
                
                _packageRepository.InstallPackages(entityPackages);
                _packageRepository.Dispose();
				context2.SaveChanges();

                _userManager.CreateUser("admin", "admin");
                _userManager.AddUserToAllRoles("admin");
				
				return RedirectToAction("Install");
			}

		}
	}
}
