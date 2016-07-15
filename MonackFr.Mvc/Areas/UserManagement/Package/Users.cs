using MonackFr.Repository;
using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonackFr.Mvc.Areas.UserManagement.Package
{
	[Export(typeof(IModule))]
	//[Export(typeof(IAuthorization))]
	public class Users : IModule, IAuthorization
	{
		#region implementation of IModule
        
		string IModule.Name
		{
			get { return "User Controller"; }
		}

		string IModule.Author
		{
			get { return "Willem de Jong"; }
		}

		string IModule.Description
		{
			get { return "Description"; }
		}

		string IModule.SystemName
		{
			get { return "MonackFr.UserManagement"; }
		}

		/// <summary>
		/// Implementation of GetMenu
		/// </summary>
		/// <returns></returns>
		IEnumerable<MenuItem> IModule.GetMenu()
		{

			List<MenuItem> menuItems = new List<MenuItem>();
			menuItems.Add(new MenuItem("Users", Names.Menus.Users.Index)
			{
				UserRoles = new string[] { UserControllerRoles.ViewUser.ToString() },
				Panel = new Panel(Names.Panels.Users.Index)
				{
					OnShow = "TestMethod",
					//Url = urlHelper.Action("Index", "User", new { Area = "UserManagement" })
				},
				MenuItems = new List<MenuItem>() 
				{
					new MenuItem("User Details", Names.Menus.Users.Details)
					{
						Visible = false,
						UserRoles = new string[] { UserControllerRoles.ViewUser.ToString() },
						Panel = new Panel(Names.Panels.Users.Details)
						{
							//Url= urlHelper.Action("Details", "User", new {Area = "UserManagement" })							
						}
					},
					new MenuItem("Add User", Names.Menus.Users.Add)
					{
						UserRoles = new string[]{UserControllerRoles.CreateUser.ToString()},
						Panel = new Panel(Names.Panels.Users.Add)
						{
							//Url = urlHelper.Action("Create", "User", new {Area = "UserManagement"})
						}
					}
				}
			});
			menuItems.Add(new MenuItem("Group", Names.Panels.Groups.Index)
			{
				Default = true,
				Panel = new Panel(Names.Panels.Groups.Index)
				{
					//Url = urlHelper.Action("Index", "Group", new { Area = "UserManagement" })
				}
			});

			return menuItems;
		}

		Tile IModule.GetTile()
		{
			IModule iModule = this;

			Tile tile = new Tile(iModule);
			tile.Title = iModule.Name;
			tile.Copyright = iModule.Author;

			return tile;
		}

		#endregion //implementation of IMenu

		#region of IAuthorization

		public List<IMfrRole> GetRoles()
		{
			List<IMfrRole> roles = new List<IMfrRole>();

			foreach (UserControllerRoles role in Enum.GetValues(typeof(UserControllerRoles)))
			{
				roles.Add(new MfrRole()
				{
					Name = role.ToString(),
					Description = role.ToDescription()
				});
			}

			return roles;
		}

		#endregion //implementation of IAuthorization
	}
}