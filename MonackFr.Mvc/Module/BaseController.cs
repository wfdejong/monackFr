using MonackFr.Mvc.Areas.PackageManagement.Entities;
using MonackFr.Mvc.Areas.PackageManagement.Repositories;
using MonackFr.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MonackFr.Mvc.Module
{	
	/// <summary>
	/// Provides functionality used for all actions
	/// </summary>
	public abstract class BaseController : Controller
	{		
		/// <summary>
		/// currently logged in user
		/// </summary>
		protected MfrUser LoggedInUser
		{
			get
			{
				if (!string.IsNullOrEmpty(User.Identity.Name))
				{
					IMfrMembershipProvider membership = Membership.Provider as IMfrMembershipProvider;
					return membership.GetMfrUser(User.Identity.Name);
				}
				
				return null;				
			}
		}
		
		/// <summary>
		/// Loads all install modules
		/// </summary>
		private void LoadInstalledModules()
		{
			//only load plugins the first time. Since pluginloader is singleton, the plugins are present if loaded once.
			if (PluginLoader.Instance.Plugins.Count() == 0)
			{
				PackageRepository packageRepository = new PackageRepository();
				
				foreach (Package package in packageRepository.GetAll())
				{
					package.LoadModules();
					if (package.Modules != null && package.Modules.Count() > 0)
					{
						PluginLoader.Instance.AddRange(package.Modules);
					}
				}
			}
		}

		/// <summary>
		/// returns menu items of all installed modules
		/// </summary>
		/// <returns></returns>
		private IEnumerable<MenuItem> GetMenuItems()
		{
			List<MenuItem> menuItems = new List<MenuItem>();

				foreach (IModule module in PluginLoader.Instance.Plugins)
				{
					menuItems.Add(module.GetMenu());
				}			

			return menuItems;
		}
		
		/// <summary>
		/// overrides OnActionExecuting. 
		/// Loads installed modules and sets MenuItems and LoggedInUser to view
		/// </summary>
		/// <param name="filterContext"></param>	
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
			LoadInstalledModules();			

			ViewBag.menuItems = GetMenuItems();			
			ViewBag.LoggedInUser = LoggedInUser;
		}
	}
}
