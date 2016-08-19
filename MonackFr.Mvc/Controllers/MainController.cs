using System.Linq;
using System.Web.Mvc;
using MonackFr.Mvc.Contexts;

namespace MonackFr.Mvc.Controllers
{
    /// <summary>
    /// This controller is the start controller of the application.
    /// </summary>
    public class MainController : Controller
    {
        /// <summary>
        /// Context to get packages from database
        /// </summary>
        private PackageContext _packageContext;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainController()
        {
            _packageContext = new PackageContext();
        }
        
        /// <summary>
        /// Returns the main page in which the complete application will be loaded
        /// </summary>
        /// <returns></returns>
		public ActionResult Index()
		{
		    ViewBag.ScriptSources = (from p in _packageContext.Packages
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
