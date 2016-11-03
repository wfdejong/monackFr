using System.Web.Mvc;

namespace MonackFr.Mvc.Areas.UserManagement.Controllers
{
    public class RoleController  : Controller
    {
        /// <summary>
        /// returns view for roles list
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}