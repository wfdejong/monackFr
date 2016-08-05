using System.Web.Mvc;

namespace MonackFr.Mvc.Areas.UserManagement.Controllers
{
    public class GroupController : Controller
    {
        #region views

        /// <summary>
        /// returns view for users list
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #endregion //views
    }
}