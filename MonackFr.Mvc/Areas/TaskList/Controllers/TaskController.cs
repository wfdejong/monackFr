using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonackFr.Mvc.Areas.TaskList.Models;
using System.ComponentModel.Composition;

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
			var tasks = _repository.GetAll();
            return View(tasks);
        }

		public ActionResult Edit(int id = 0)
		{
			Task task = _repository.FindBy(t => t.Id == id).First<Task>();
			return View(task);
		}

		[HttpPost]
		public ActionResult Edit(Task task)
		{
			if (ModelState.IsValid)
			{
				_repository.Edit(task);
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
		public ActionResult Create(Task task)
		{
			if (ModelState.IsValid)
			{
				_repository.Create(task);
				_repository.Save();
				return RedirectToAction("Index");
			}

			return View(task);
		}

		public ActionResult Delete(int Id)
		{
			Task task = _repository.FindBy(t => t.Id == Id).First<Task>();
			_repository.Delete(task);
			_repository.Save();
			return RedirectToAction("Index");
		}

		#endregion //view functions

		#region implementation of IModule
				
		public MenuItem GetMenu()
		{
			MenuItem menuItem = new MenuItem();
			menuItem.Text = "Tasks";

			menuItem.MenuItems = new List<MenuItem>();
			menuItem.MenuItems.Add(new MenuItem { Text = "Overview", ActionName = "Index", Controller = "Task", Area = "TaskList" });
			
			return menuItem;
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
