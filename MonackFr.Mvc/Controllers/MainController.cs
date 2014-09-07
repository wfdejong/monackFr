using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MonackFr.Module;

namespace MonackFr.Mvc.Controllers
{
    public class MainController : Controller
    {
		public ActionResult Index()
		{
			return View();
		}
    }
}
