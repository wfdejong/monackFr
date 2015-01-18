using AutoMapper;
using MonackFr.Repository;
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

		string IModule.SystemName { get { return "MonackFr.TaskManager"; } }

        IEnumerable<MenuItem> IModule.GetMenu(UrlHelper urlHelper)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            
            menuItems.Add(new MenuItem ("Overview", "MonackFr.TaskList.Menu.Index")
			{ 
				Panel = new Panel("MonackFr.TaskList.Panel.Index")
				{
					 OnShow = "jsfunction",
					 Url = urlHelper.Action("Index", "Task", new { area = "TaskList" })
				}
			});

            return menuItems;
        }

        Tile IModule.GetTile(UrlHelper url)
        {
			IModule iModule = (IModule)this;
			
            Tile tile = new Tile(iModule);
			tile.Title = iModule.Name;
            tile.Url = url.Action("Index", "Task", new { area = "TaskList" });

			IQueryable<Entities.Task> tasksEntities = _repository.GetAll();
            IEnumerable<ViewModels.Task> tasks = Mapper.Map<IEnumerable<ViewModels.Task>>(tasksEntities);
            tile.PreviewItems = (from t in tasks
                                 select t.LastUpdate.ToString()).ToArray();

			tile.Copyright = iModule.Author;

            return tile;
        }

        #endregion //implementation of IMenu
    }
}