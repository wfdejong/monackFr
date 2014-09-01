using MonackFr.Module;
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
			List<Tile> tiles = new List<Tile>();

			foreach (IModule module in ModuleKeeper.Instance.Modules)
			{
				tiles.Add(module.GetTile(Url));
			}

			return Json(tiles);
		}

		public JsonResult GetTile(string moduleName)
		{
			IModule module = ModuleKeeper.Instance.GetModule(moduleName);
			return Json(module.GetTile(Url));
		}
    }
}
