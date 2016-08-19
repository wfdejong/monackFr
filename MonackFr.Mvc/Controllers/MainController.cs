using System.Linq;
using System.Web.Mvc;
using MonackFr.Mvc.Contexts;
using MonackFr.Mvc.Repositories;

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
        private PackageRepository _packageRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainController()
        {
            _packageRepository = new PackageRepository();
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
    }
}
