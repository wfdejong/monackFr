using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonackFr.Mvc.Controllers
{
    public class TilesController : BaseController
    {

		/// <summary>
		/// returns tiles
		/// </summary>
		/// <returns></returns>
		public JsonResult GetTiles()
		{
			List<Module.Tile> tiles = new List<Module.Tile>();

			foreach (Module.IModule module in ModuleKeeper.Instance.Modules)
			{
				tiles.Add(module.GetTile(Url));
			}

			IEnumerable<ViewModels.Tile> viewModelTiles = Mapper.Map<IEnumerable<ViewModels.Tile>>(tiles);

			return Json(viewModelTiles);
		}

		public JsonResult GetTile(string moduleName)
		{
			Module.IModule module = ModuleKeeper.Instance.GetModule(moduleName);
			return Json(module.GetTile(Url));
		}
    }
}
