using MonackFr.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonackFr.Mvc.Areas.TaskList
{
    [Export(typeof(IModule))]
    public class TaskListModule : IModule
    {
        #region implementation of IModule

        string IModule.Name { get { return "TaskList"; } }

        string IModule.Description { get { return "TaskList"; } }

        string IModule.Author { get { return "Willem de Jong"; } }

        MenuItem IModule.GetMenu()
        {
            MenuItem menuItem = new MenuItem();
            menuItem.Text = "Tasks";

            menuItem.MenuItems = new List<MenuItem>();
            menuItem.MenuItems.Add(new MenuItem { Text = "Overview", ActionName = "Index", Controller = "Task", Area = "TaskList" });

            return menuItem;
        }

        Tile IModule.GetTile(UrlHelper url)
        {
            Tile tile = new Tile();
            tile.Title = "TaskList";
            tile.Url = url.Action("Index", "Task", new { area = "TaskList" });

            tile.PreviewItems = new string[] {
					 "Pay bills today @ 21:23",
					 "Watch T.V. @ 21-2-2014 13:45"
				 };
            tile.Copyright = "The Monack Framework";

            return tile;
        }

        #endregion //implementation of IMenu
    }
}