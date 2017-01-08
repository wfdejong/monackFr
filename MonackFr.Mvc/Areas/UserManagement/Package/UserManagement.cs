using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using MonackFr.Library.Module;

namespace MonackFr.Mvc.Areas.UserManagement.Package
{
	[Export(typeof(IModule))]
	//[Export(typeof(IAuthorization))]
	public class UserManagement : IModule, IAuthorization
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

	    IEnumerable<State> IModule.States
	    {
	        get
	        {
	            return new List<State>
	            {
	                new State
	                {
	                    Name = "monackfr-usermanagement-users",
	                    Config =
                            @"url: '/module/usermanagement', 
                            views: {
                                'menu' : {templateUrl: 'usermanagement/menu/index', controller: 'menuController', controllerAs: 'testCtrl'},
                                'content' : {templateUrl: 'usermanagement/user/index', controller: 'usersController', controllerAs: 'usersCtrl'}
                            }"
                    },
                    new State
                    {
                        Name = "monackfr-usermanagement-newuser",
                        Config =
                            @"url: '/module/usermanagement/newuser',
                            views: {
                                'menu' : { templateUrl: 'usermanagement/menu/index', controller: 'menuController', controllerAs: 'testCtrl'},
                                'content' : { templateUrl: 'usermanagement/user/new', controller: 'newUserController', controllerAs: 'newUserCtrl'}
                            }"
                    },
                    new State
                    {
                        Name = "monackfr-usermanagement-edituser",
                        Config =
                            @"url: '/module/usermanagement/edituser/:id', 
                            views: {
                                'menu' :{ templateUrl: 'usermanagement/menu/index', controller: 'menuController', controllerAs: 'testCtrl'},
                                'content' : { templateUrl: 'usermanagement/user/edit', controller: 'editUserController', controllerAs: 'editUserCtrl'}
                            }"
                    },
                     new State
                    {
                        Name = "monackfr-usermanagement-roles",
                        Config =
                            @"url: '/module/usermanagement/roles', 
                            views: {
                                'menu' :{ templateUrl: 'usermanagement/menu/index', controller: 'menuController', controllerAs: 'testCtrl'},
                                'content' : { templateUrl: 'usermanagement/role/index', controller: 'rolesController', controllerAs: 'rolesCtrl'}
                            }"
                    },
                    new State
                    {
                        Name = "monackfr-usermanagement-groups",
                        Config =
                            @"url: '/module/usermanagement/groups',
                            views: {
                                'menu' :{ templateUrl: 'usermanagement/menu/index', controller: 'menuController', controllerAs: 'testCtrl'},
                                'content' : { templateUrl: 'usermanagement/group/index', controller: 'groupsController', controllerAs: 'groupsCtrl'}
                            }"
                    },
                    new State
                    {
                        Name = "monackfr-usermanagement-newgroup",
                        Config =
                            @"url: '/module/usermanagement/newgroup',
                            views: {
                                'menu' :{templateUrl: 'usermanagement/menu/index', controller: 'menuController', controllerAs: 'testCtrl'},
                                'content' : {templateUrl: 'usermanagement/group/new', controller: 'newGroupController', controllerAs: 'newGroupCtrl'}
                            }"
                    },
                    new State
                    {
                        Name = "monackfr-usermanagement-editgroup",
                        Config =
                            @"url: '/module/usermanagement/groupuser/:id',
                            views: {
                                'menu' :{templateUrl: 'usermanagement/menu/index', controller: 'menuController', controllerAs: 'testCtrl'},
                                'content' : {templateUrl: 'usermanagement/group/edit', controller: 'editGroupController', controllerAs: 'editGroupCtrl'}
                            }"
                    }
                };
	        }
	    }

		Tile IModule.GetTile()
		{
			IModule iModule = this;

			Tile tile = new Tile();
			tile.Title = iModule.Name;
			tile.Copyright = iModule.Author;
		    tile.Controller = "monackfr-usermanagement-users";
            
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