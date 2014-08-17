using AutoMapper;
using MonackFr.Module;
using MonackFr.Mvc.Areas.TaskList.Repositories;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace MonackFr.Mvc.Areas.TaskList.Controllers
{
	[Export(typeof(IModule))]
    public class TaskController : BaseController, IModule
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
		public TaskController()
		{
			_repository = new TaskListRepository();
		}

		/// <summary>
		/// Initialize with custom repository
		/// </summary>
		/// <param name="repository"></param>
		public TaskController(ITaskListRepository repository)
		{
			_repository = repository;
		}

		#endregion

		#region View functions

		/// <summary>
		/// Get: /Tasklist/
		/// </summary>
		/// <returns></returns>
        public ActionResult Index()
        {			
			IEnumerable<ViewModels.Task> tasks = Mapper.Map<IEnumerable<ViewModels.Task>>(_repository.GetAll());
            return View(tasks);
        }

		public ActionResult Edit(int id = 0)
		{
			ViewModels.Task task = Mapper.Map<ViewModels.Task>(_repository.FindBy(t => t.Id == id).First<Entities.Task>());
			return View(task);
		}

		[HttpPost]
		public ActionResult Edit(ViewModels.Task task)
		{
			if (ModelState.IsValid)
			{                
				_repository.Edit(Mapper.Map<Entities.Task>(task));
				_repository.Save();
				return RedirectToAction("Index");
			}

			return View(task);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(ViewModels.Task task)
		{
			if (ModelState.IsValid)
			{
				_repository.Create(Mapper.Map<Entities.Task>(task));
				_repository.Save();
				return RedirectToAction("Index");
			}

			return View(task);
		}

		public ActionResult Delete(int Id)
		{
			Entities.Task task = _repository.FindBy(t => t.Id == Id).First<Entities.Task>();
			_repository.Delete(task);
			_repository.Save();
			return RedirectToAction("Index");
		}

		#endregion //view functions

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

    public Dictionary<string, string> MetaData
    {
        get
        {
            Dictionary<string, string> MetaData = new Dictionary<string, string>();
            MetaData.Add("Name", "TaskPlugin");
            MetaData.Add("Author", "Willem de Jong");

            return MetaData;
        }
    }	

		#endregion //implementation of IMenu
	}
}
