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
        //
        // GET: /Tiles/

        public ActionResult Index()
        {
			ViewBag.Tiles = this.GetTiles();
            return View();
        }
    }
}
