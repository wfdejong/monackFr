using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MonackFr.Repository;
using MonackFr.Mvc.Repositories;

namespace MonackFr.Mvc.Controllers
{
    public class MainController : Controller
    {
		IModuleRepository _moduleRepository;

		public MainController()
		{
			_moduleRepository = new ModuleRepository();
		}

		/// <summary>
		/// Constructor for unit testing
		/// </summary>
		/// <param name="iModuleRepository"></param>
		internal MainController(IModuleRepository iModuleRepository)
		{
			_moduleRepository = iModuleRepository;
		}
				
		public ActionResult Index()
		{
			ViewBag.Modules = Mapper.Map<IEnumerable<ViewModels.Module>>(_moduleRepository.GetAll());
			return View();
		}
    }
}
