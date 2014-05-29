using MonackFr.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonackFr.Mvc
{
	/// <summary>
    /// Disposes all added disposable items at OnActionExecuted event    
	/// </summary>
    public abstract class DisposeController 
		: BaseController 		
    {
		/// <summary>
		/// Disposable items
		/// </summary>
		private List<IDisposable> _disposables;

		/// <summary>
		/// Adds a disposable item to the list
		/// </summary>
		/// <param name="disposable"></param>
		protected void AddDisposable(IDisposable disposable)
		{
			_disposables.Add(disposable);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public DisposeController()
		{
			_disposables = new List<IDisposable>();
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="disposables"></param>
		public DisposeController(params IDisposable[] disposables) 
            : this()
		{            
			//TODO: check if parameterless constructor is hit, so _disposables is not null
			foreach (IDisposable disposable in disposables)
			{
				AddDisposable(disposable);
			}
		}

		/// <summary>
		/// Overrides OnActionExecuted event. Disposes all items in list.
		/// </summary>
		/// <param name="filterContext"></param>
		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			foreach (IDisposable disposable in _disposables)
			{
				disposable.Dispose();
			}
			_disposables = null;
			
			base.OnActionExecuted(filterContext);
		}
	}
}
