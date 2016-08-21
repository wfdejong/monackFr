﻿using System.Web.Mvc;

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