using System.Web.Mvc;

namespace MonackFr.Mvc.Areas.UserManagement.Controllers
{
    public class UserController : Controller
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

        /// <summary>
        /// returns view for new user
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            return View();
        }

        /// <summary>
        /// returns view for edit user
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }

        #endregion //views
    }
}