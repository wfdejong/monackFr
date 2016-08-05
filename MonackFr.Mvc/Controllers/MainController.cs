using System.Web.Mvc;

namespace MonackFr.Mvc.Controllers
{
    public class MainController : Controller
    {				
		public ActionResult Index()
		{
			return View();
		}

        public ActionResult Tile()
        {
            return View();
        }
    }
}
