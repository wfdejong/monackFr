using MonackFr.Repository;
using MonackFr.Mvc.Areas.UserManagement.Providers;
using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MonackFr.Mvc.Areas.UserManagement.Controllers
{
	[Export(typeof(IModule))]
	[Export(typeof(IAuthorization))]
	public class UserController : BaseController, IModule, IAuthorization
	{
		private enum UserControllerRoles
		{
			[Description("View Users")]
			ViewUser,

			[Description("Edit users")]
			EditUser,

			[Description("Create users")]
			CreateUser,

			[Description("Delete users")]
			DeleteUser
		};
		
		#region private properties

		/// <summary>
		/// The repository
		/// </summary>
		private MonackFr.Mvc.Repositories.IUserRepository _repository;

		/// <summary>
		/// Authentication object
		/// </summary>
		private IAuthentication _authentication;

		#endregion //private properties

		#region constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public UserController()
		{
			_repository = new MonackFr.Mvc.Repositories.UserRepository();
			_authentication = new Authentication();
		}

		/// <summary>
		/// Constructor with custom repository and authentication object
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="authentication"></param>
		public UserController(MonackFr.Mvc.Repositories.IUserRepository repository, IAuthentication authentication)
		{
			_repository = repository;
			_authentication = authentication;
		}

		#endregion //constructors

		#region public methods
		//
		// GET: /User/
		//[Role(UserControllerRoles.ViewUser)]
		public ActionResult Index()
		{
			var users = _repository.GetAll();			
			return View(users);
		}

		//
		// Get: /User/Login/
		public ViewResult Login()
		{
			return View();
		}

		//
		// Get: /Users/user/Logout/
		public ViewResult Logout()
		{
			//_authentication.SignOut();
			//return View("Login");
			return View();
		}
		
		//
		// Post: /User/Login/
		[HttpPost]
		public ActionResult Login(ViewModels.LoginUser loginUser, String returnUrl)
		{
			MembershipProvider membershipProvider = new MfrMembershipProvider(_repository);
			if (membershipProvider.ValidateUser(loginUser.UserName, loginUser.Password))
			{
				_authentication.SetAuthCookie(loginUser.UserName, false);

				if (Url.IsLocalUrl(returnUrl))
				{
					return Redirect(returnUrl);
				}
				else
				{
					//TODO: show error
					throw new NotImplementedException();
				}
			}
			
			return View();
			
		}

		//
		// GET: /User/Details/5
		public ViewResult Details2(int id)
		{
			Entities.User user = _repository.GetSingle(u => u.Id == id);
			MonackFr.Mvc.Repositories.IRoleRepository roleRepository = new MonackFr.Mvc.Repositories.RoleRepository();
			List<Entities.Role> roles = roleRepository.GetAll().ToList<Entities.Role>();

			MonackFr.Mvc.Repositories.IGroupRepository groupRepository = new MonackFr.Mvc.Repositories.GroupRepository();
			List<Entities.Group> groups = groupRepository.GetAll().ToList<Entities.Group>();

			ViewModels.DetailsUser detailsUser = new ViewModels.DetailsUser();

			detailsUser.Fill(user);
			detailsUser.UserRoles = LoadRoles(roles, user.Roles.AsQueryable<Entities.Role>()); //automapper

			detailsUser.UserGroups = LoadGroups(groups, user.Groups.AsQueryable<Entities.Group>()); //automapper
			
			return View(detailsUser);
		}

		public ViewResult Details()
		{
			return View();
		}
				
		//
		// GET: /User/Create
		public ActionResult Create()
		{
			return View();
		}

		//
		// POST: /User/Create
		[HttpPost]
		public ActionResult Create(ViewModels.CreateUser createUser)
		{
			if (ModelState.IsValid)
			{	
				Membership.CreateUser(createUser.UserName, createUser.Password, createUser.Email);
				return RedirectToAction("Index");
			}
			else
			{
				return View(createUser);
			}

		}

		//
		// GET: /User/Edit/5
		public ViewResult Edit(int id)
		{
			Entities.User user = _repository.GetSingle(u => u.Id == id);

			ViewModels.DetailsUser detailsUser = new ViewModels.DetailsUser();
			detailsUser.Fill(user);

			MonackFr.Mvc.Repositories.IRoleRepository roleRepository = new MonackFr.Mvc.Repositories.RoleRepository();
			List<Entities.Role> roles = roleRepository.GetAll().ToList<Entities.Role>();
			detailsUser.UserRoles = LoadRoles(roles, user.Roles.AsQueryable<Entities.Role>());	

			MonackFr.Mvc.Repositories.IGroupRepository groupRepository = new MonackFr.Mvc.Repositories.GroupRepository();
			List<Entities.Group> groups = groupRepository.GetAll().ToList<Entities.Group>();
			detailsUser.UserGroups = LoadGroups(groups, user.Groups.AsQueryable<Entities.Group>());
			
			return View(detailsUser);
		}

		//
		// POST: /User/Edit/5
		[HttpPost]
		public ActionResult Edit(ViewModels.DetailsUser detailsUser)
		{
			if (ModelState.IsValid)
			{
				Mvc.Entities.User user = _repository.GetSingle(u => u.Id == detailsUser.Id);
				detailsUser.Map(user);

				MonackFr.Mvc.Repositories.IRoleRepository roleRepository = new MonackFr.Mvc.Repositories.RoleRepository();
				IQueryable<Entities.Role> queryableRoles = roleRepository.GetAll();
				IEnumerable<string> numerableRoles = from r in queryableRoles select r.Name;
				String[] allRoles = numerableRoles.ToArray<String>();
								
				//remove all current roles from user.
				Roles.RemoveUserFromRoles(user.UserName, allRoles);
				
				//add roles to user
				foreach (ViewModels.CheckBoxListRole userRole in detailsUser.UserRoles)
				{

					if (userRole.Checked)
					{
						Roles.AddUserToRole(user.UserName, userRole.Name);
					}
				}

				//remove all groups from user
				_repository.RemoveAllGroupsFromUser(user);
							
				//add group to user
				if (detailsUser.UserGroups != null)
				{
					foreach (ViewModels.CheckBoxListGroup userGroup in detailsUser.UserGroups)
					{
						if (userGroup.Checked)
						{
							Entities.Group group = new Entities.Group() { Id = userGroup.Id };
							_repository.AddGroupsToUser(user, group);
						}
					}
				}

				_repository.Edit(user);
				_repository.Save();


				return RedirectToAction("Details", detailsUser);
			}
			else
			{
				return View(detailsUser);
			}
		}

		//
		// POST: /User/Delete/5
		[HttpPost]
		public ActionResult Delete(int id)
		{
			Entities.User user = _repository.GetSingle(u => u.Id == id);
			
			if (user != null)
			{
				_repository.Delete(user);
				_repository.Save();
			}
			
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult AddRole(int userId, int allGroups)
		{
			Entities.User user = _repository.GetSingle(u => u.Id == userId);
			return View(user);
		}

		#endregion //public methods

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
		IEnumerable<MenuItem> IModule.GetMenu(UrlHelper urlHelper)
		{
			
			List<MenuItem> menuItems = new List<MenuItem>();
			menuItems.Add(new MenuItem("Users", "MonackFr.UserManagement.Users.Index")
			{
				UserRoles = new string[] { UserControllerRoles.ViewUser.ToString() },
				Panel = new Panel("MonackFr.UserManagerment.Users.Panel.Index")
				{
					OnShow = "TestMethod",
					Url = urlHelper.Action("Index", "User", new { Area = "UserManagement" })
				},
				MenuItems = new List<MenuItem>() 
				{
					new MenuItem("User Details", "MonackFr.UserManagement.Users.UserDetails")
					{
						Visible = false,
						UserRoles = new string[] { UserControllerRoles.ViewUser.ToString() },
						Panel = new Panel("MonackFr.UserManagement.Users.Panel.UserDetails")
						{
							Url= urlHelper.Action("Details", "User", new {Area = "UserManagement" })							
						}
					}
				}
			});
			menuItems.Add(new MenuItem("Group", "MonackFr.UserManagement.Group.Index")
			{
				Default = true,
				Panel = new Panel("MonackFr.UserManagerment.Group.Panel.Index")
				{
					Url = urlHelper.Action("Index", "Group", new { Area = "UserManagement" })
				}
			});
			
			return menuItems;
		}

		Repository.Tile IModule.GetTile(UrlHelper url)
		{
			IModule iModule = (IModule)this;

            Repository.Tile tile = new Repository.Tile(iModule);
			tile.Title = iModule.Name;
			tile.Url = url.Action("Index", "User", new { area = "UserManagement" });
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
				roles.Add(new MfrRole() {
					Name = role.ToString(),
					Description = role.ToDescription()
				});
			}

			return roles;
		}

		#endregion //implementation of IAuthorization

		#region private methods

		private List<ViewModels.CheckBoxListRole> LoadRoles(List<Entities.Role> allRoles, IQueryable<Entities.Role> selectedRoles)
		{
			List<ViewModels.CheckBoxListRole> userRoles = new List<ViewModels.CheckBoxListRole>();
			
			foreach (Entities.Role role in allRoles)
			{
				userRoles.Add(new ViewModels.CheckBoxListRole
				{
					Id = role.Id,
					Name = role.Name,
					Description = role.Description,
					Checked = (selectedRoles.FirstOrDefault<Entities.Role>(r => r.Id == role.Id) != null)
				});
			}

			return userRoles;

		}

		private List<ViewModels.CheckBoxListGroup> LoadGroups(List<Entities.Group> allGroups, IQueryable<Entities.Group> selectedGroups)
		{
			List<ViewModels.CheckBoxListGroup> userGroups = new List<ViewModels.CheckBoxListGroup>();
			
			foreach (Entities.Group group in allGroups)
			{
				userGroups.Add(new ViewModels.CheckBoxListGroup
				{
					Id = group.Id,
					Name = group.Name,
					Description = group.Description,
					Checked = (selectedGroups.FirstOrDefault<Entities.Group>(g => g.Id == group.Id) != null)
				});
			}

			return userGroups;
		}

		#endregion //private methods
	}
}
