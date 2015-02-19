using AutoMapper;
using MonackFr.Repository;
using MonackFr.Mvc.Repositories;
using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MonackFr.Mvc
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
		/// overrides OnActionExecuting. 
		/// Loads installed modules and sets MenuItems and LoggedInUser to view
		/// </summary>
		/// <param name="filterContext"></param>	
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
					
			ViewBag.LoggedInUser = LoggedInUser;
		}

		/// <summary>
		/// Adds default viewbag data and returns view with model. 
		/// </summary>
		/// <param name="model">the model</param>
		/// <returns></returns>
		protected virtual ViewResult MfrView(object model, string panelName)
		{
			ViewBag.PanelJsName = Format.ToJs(panelName);			
			return View(model);
		}
	}
}
