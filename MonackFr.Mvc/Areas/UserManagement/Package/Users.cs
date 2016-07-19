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

		List<IMfrRole> IAuthorization.GetRoles()
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