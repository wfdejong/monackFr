using System.Web.Mvc;

namespace MonackFr.Mvc.Areas.UserManagement.Controllers
{
    public class GroupController : Controller
    {
        #region views

        /// <summary>
        /// returns view for groups list
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// returns view for new group
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            return View();
        }

        /// <summary>
        /// returns view for edit group
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }

        #endregion //views
    }
}