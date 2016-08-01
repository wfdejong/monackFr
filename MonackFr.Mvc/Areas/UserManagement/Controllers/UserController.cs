using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonackFr.Security;

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

        #endregion //views
    }
}