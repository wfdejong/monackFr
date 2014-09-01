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

		/// <summary>
		/// Returns list of loaded modules
		/// </summary>
		/// <returns></returns>
		public JsonResult Modules()
		{
			//TODO ViewModel.Module should be transferred to mvc.viewmodels since this is a application wide model
			// There should met an entity for module/package
			IEnumerable<MonackFr.Mvc.Areas.PackageManagement.ViewModels.Module> modules = Mapper.Map<IEnumerable<MonackFr.Mvc.Areas.PackageManagement.ViewModels.Module>>(ModuleKeeper.Instance.Modules);
			return Json(modules);
		}

		public JsonResult Menu(string moduleName)
		{
			IModule module = ModuleKeeper.Instance.GetModule(moduleName);
			MenuItem menu = module.GetMenu();
			return Json(Mapper.Map<ViewModels.MenuItem>(menu)); //should be a viewmodule in mvc.viewmodels
		}
    }
}
