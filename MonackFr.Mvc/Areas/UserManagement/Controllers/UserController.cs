using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonackFr.Mvc.Areas.UserManagement.Controllers
{
    public class UserController : Controller
    {
        // GET: UserManagement
        public ActionResult Index()
        {
            return View();
        }
    }
}