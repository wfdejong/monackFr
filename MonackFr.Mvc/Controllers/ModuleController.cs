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
		private ModuleRepository _moduleRepository = null;

		/// <summary>
		/// Constructor
		/// </summary>
		public ModuleController() : base()
		{
			_moduleRepository = new ModuleRepository();

			//Add disposables			
			AddDisposable(_moduleRepository as IDisposable);
		}

		/// <summary>
		/// Returns all IModules
		/// </summary>
		/// <returns></returns>
		public JsonResult GetModules()
		{
			IEnumerable<ViewModels.Module> modules = Mapper.Map<IEnumerable<ViewModels.Module>>(_moduleRepository.GetAll());
			return Json(modules);
		}

		/// <summary>
		/// Returns menu items of a IModule
		/// </summary>
		/// <param name="IModuleName"></param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult GetMenu(string systemName)
		{
			return Json(ModuleKeeper.Instance.GetModule(systemName).GetMenu(Url));
		}
	}
}