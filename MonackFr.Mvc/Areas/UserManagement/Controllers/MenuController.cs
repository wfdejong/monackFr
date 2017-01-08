using System.Web.Mvc;

namespace MonackFr.Mvc.Areas.UserManagement.Controllers
{
    public class MenuController : Controller
    {
        #region views

        /// <summary>
        /// returns menu
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #endregion //views
    }
}