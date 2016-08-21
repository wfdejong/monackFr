using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MonackFr.Library.Module;
using MonackFr.Mvc.Contexts;
using MonackFr.Mvc.Repositories;
using MonackFr.Library.Repository;
using MonackFr.Mvc.DatabaseManagement;
using MonackFr.Mvc.PackageManagement;
using WebGrease.Css.Extensions;

namespace MonackFr.Mvc.Controllers
{
    /// <summary>
    /// This controller is the start controller of the application.
    /// </summary>
    public class MainController : Controller
    {
        /// <summary>
        /// repository for packages
        /// </summary>
        private IPackageRepository _packageRepository;

        /// <summary>
        /// package manager
        /// </summary>
        private IPackageManager _packageManager;

        /// <summary>
        /// database manager
        /// </summary>
        private IDatabaseManager _databaseManager;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainController()
        {
            _packageRepository = new PackageRepository();
            _packageManager = new PackageManager();
            _databaseManager = new DatabaseManager();
        }
        
        /// <summary>
        /// Returns the main page in which the complete application will be loaded
        /// </summary>
        /// <returns></returns>
		public ActionResult Index()
		{
		    ViewBag.ScriptSources = (from p in _packageRepository.GetAll()
		        where p.ScriptSource != null
		        select p.ScriptSource).ToList();

            var states = new List<State>();

            (from m in ModuleKeeper.Instance.Modules
                where m.States != null
                select m.States).ForEach(x=> states.AddRange(x));

            ViewBag.States = states;
            
			return View();
		}

        /// <summary>
        /// Returns the template of a tile
        /// </summary>
        /// <returns></returns>
        public ActionResult Tile()
        {
            return View();
        }

        public ActionResult Install()
        {
            return View();
        }

        /// <summary>
        /// Installs complete new database
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Install(FormCollection formCollection)
        {
            string baseDirectory = string.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
            string packageDirectory = string.Format("{0}{1}\\", baseDirectory, ApplicationSettings.PackageDir);
            var mapper = AutoMapperConfig.Mapper;
            
            using (Context context = new Context())
            {
                IEnumerable<Package> packages = _packageManager.GetPackages(packageDirectory, baseDirectory);
                List<IContext> contexts = new List<IContext>
                {
                    new PackageContext(),
                    new UserManagementContext()
                };

                foreach (Package package in packages)
                {
                    contexts.AddRange(package.Contexts);
                }

                //install database
                _databaseManager.InstallDatabase(contexts);
                
                //save packages
                IEnumerable<Entities.Package> entityPackages = mapper.Map<IEnumerable<Entities.Package>>(packages);

                _packageRepository.InstallPackages(entityPackages);
                _packageRepository.Dispose();
                context.SaveChanges();

                //_userManager.CreateUser("admin", "admin");
                //_userManager.AddUserToAllRoles("admin");

                return RedirectToAction("Install");
            }
        }
    }
}
