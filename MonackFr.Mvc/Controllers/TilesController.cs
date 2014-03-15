using MonackFr.Mvc.Module;
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

			foreach (IModule module in PluginLoader.Instance.Plugins)
			{
				tiles.Add(module.GetTile(Url));
			}

			return Json(tiles);
		}
    }
}
