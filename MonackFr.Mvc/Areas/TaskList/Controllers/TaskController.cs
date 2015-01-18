using AutoMapper;
using MonackFr.Repository;
using MonackFr.Mvc.Areas.TaskList.Repositories;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace MonackFr.Mvc.Areas.TaskList.Controllers
{	
    public class TaskController : BaseController
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
	}
}
