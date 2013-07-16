using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.Composition;

namespace MonackFr.Mvc.Areas.TaskListArea.Controllers
{
	[Export(typeof(IModule))]
    public class DemoProjectController: IModule
    {
        //
        // GET: /TaskListArea/TaskList/

		//public ActionResult Index()
		//{
		//	//return View();
		//}

		public MenuItem GetMenu()
		{
			MenuItem menuItem = new MenuItem();
			menuItem.Text = "Test";
			return menuItem;
		}

        public Dictionary<string, string> MetaData
        {
            get
            {
                Dictionary<string, string> MetaData = new Dictionary<string, string>();
                MetaData.Add("Name", "DemoProject");
                MetaData.Add("Author", "Willem de Jong");

                return MetaData;
            }
        }

    }
}
