using AutoMapper;
using MonackFr.Module;
using MonackFr.Mvc.Areas.TaskList.Repositories;
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
        #region private properties

		/// <summary>
		/// Repository
		/// </summary>
		private ITaskListRepository _repository;

		#endregion //private properties

		#region constructors

		/// <summary>
		/// Initialize with default repository
		/// </summary>
		public TaskListModule()
		{
			_repository = new TaskListRepository();
		}

		/// <summary>
		/// Initialize with custom repository
		/// </summary>
		/// <param name="repository"></param>
        public TaskListModule(ITaskListRepository repository)
		{
			_repository = repository;
		}

		#endregion

        #region implementation of IModule

        string IModule.Name { get { return "TaskList"; } }

        string IModule.Description { get { return "TaskList"; } }

        string IModule.Author { get { return "Willem de Jong"; } }

        MenuItem IModule.GetMenu()
        {
            MenuItem menuItem = new MenuItem();
            menuItem.Label = "Tasks";

            menuItem.MenuItems = new List<MenuItem>();
            menuItem.MenuItems.Add(new MenuItem { Label = "Overview", Action = "Index", Controller = "Task", Area = "TaskList" });

            return menuItem;
        }

        Tile IModule.GetTile(UrlHelper url)
        {
            Tile tile = new Tile();
            tile.Title = "TaskList";
            tile.Url = url.Action("Index", "Task", new { area = "TaskList" });

            IEnumerable<ViewModels.Task> tasks = Mapper.Map<IEnumerable<ViewModels.Task>>(_repository.GetAll());
            tile.PreviewItems = (from t in tasks
                                 select t.LastUpdate.ToString()).ToArray();
                        
            tile.Copyright = "The Monack Framework";

            return tile;
        }

        #endregion //implementation of IMenu
    }
}