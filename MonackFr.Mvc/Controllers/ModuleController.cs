using AutoMapper;
using MonackFr.Mvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonackFr.Mvc.Controllers
{
	public class ModuleController : DisposeController
	{
		private ModuleRepository _moduleRepository = new ModuleRepository();

		public ModuleController() : base()
		{
			//Add disposables here
			AddDisposable(_moduleRepository);
		}

		/// <summary>
		/// Returns all modules
		/// </summary>
		/// <returns></returns>
		public JsonResult GetModules()
		{
			IEnumerable<ViewModels.Module> modules = Mapper.Map<IEnumerable<ViewModels.Module>>(_moduleRepository.GetAll());
			return Json(modules);
		}
	}
}