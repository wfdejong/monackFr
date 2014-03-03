using MonackFr.Mvc.Areas.UserManagement.Models;
using MonackFr.Mvc.Areas.UserManagement.Providers;
using MonackFr.Mvc.Areas.UserManagement.Repositories;
using MonackFr.Mvc.Areas.UserManagement.ViewModels;
using MonackFr.Mvc.Module;
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
		private IUserRepository _repository;

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
			_repository = new UserRepository();
			_authentication = new Authentication();
		}

		/// <summary>
		/// Constructor with custom repository and authentication object
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="authentication"></param>
		public UserController(IUserRepository repository, IAuthentication authentication)
		{
			_repository = repository;
			_authentication = authentication;
		}

		#endregion //constructors

		#region public methods
		//
		// GET: /User/
		[Role(UserControllerRoles.ViewUser)]
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
			_authentication.SignOut();
			return View("Login");
		}
		
		//
		// Post: /User/Login/
		[HttpPost]
		public ActionResult Login(LoginUser loginUser, String returnUrl)
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
		public ViewResult Details(int id)
		{
			User user = _repository.GetSingle(u => u.Id == id);			
			IRoleRepository roleRepository = new RoleRepository();
			List<Role> roles = roleRepository.GetAll().ToList<Role>();
			
			IGroupRepository groupRepository = new GroupRepository();
			List<Group> groups = groupRepository.GetAll().ToList<Group>();

			DetailsUser detailsUser = new DetailsUser();

			detailsUser.Fill(user);
			detailsUser.UserRoles = LoadRoles(roles, user.Roles.AsQueryable<Role>());

			detailsUser.UserGroups = LoadGroups(groups, user.Groups.AsQueryable<Group>());
			
			return View(detailsUser);
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
		public ActionResult Create(CreateUser createUser)
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
			User user = _repository.GetSingle(u => u.Id == id);

			DetailsUser detailsUser = new DetailsUser();
			detailsUser.Fill(user);

			IRoleRepository roleRepository = new RoleRepository();
			List<Role> roles = roleRepository.GetAll().ToList<Role>();
			detailsUser.UserRoles = LoadRoles(roles, user.Roles.AsQueryable<Role>());			

			IGroupRepository groupRepository = new GroupRepository();
			List<Group> groups = groupRepository.GetAll().ToList<Group>();
			detailsUser.UserGroups = LoadGroups(groups, user.Groups.AsQueryable<Group>());
			
			return View(detailsUser);
		}

		//
		// POST: /User/Edit/5
		[HttpPost]
		public ActionResult Edit(DetailsUser detailsUser)
		{
			if (ModelState.IsValid)
			{
				User user = _repository.GetSingle(u => u.Id == detailsUser.Id);
				detailsUser.Map(user);
								
				IRoleRepository roleRepository = new RoleRepository();
				IQueryable<Role> queryableRoles = roleRepository.GetAll();
				IEnumerable<string> numerableRoles = from r in queryableRoles select r.Name;
				String[] allRoles = numerableRoles.ToArray<String>();
								
				//remove all current roles from user.
				Roles.RemoveUserFromRoles(user.UserName, allRoles);
				
				//add roles to user
				foreach (CheckBoxListRole userRole in detailsUser.UserRoles)
				{

					if (userRole.Checked)
					{
						Roles.AddUserToRole(user.UserName, userRole.Name);
					}
				}

				//remove all groups from user
				_repository.RemoveAllGroupsFromUser(user);
				
				//add group to user
				foreach (CheckBoxListGroup userGroup in detailsUser.UserGroups)
				{
					if (userGroup.Checked)
					{
						Group group = new Group() { Id = userGroup.Id };
						_repository.AddGroupsToUser(user, group);
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
			User user = _repository.GetSingle(u => u.Id == id);
			
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
			User user = _repository.GetSingle(u => u.Id == userId);
			return View(user);
		}

		#endregion //public methods

		#region implementation of IPlugin

		/// <summary>
		/// Implementation of GetMenu
		/// </summary>
		/// <returns></returns>
		public MenuItem GetMenu()
		{
			MenuItem menuItem = new MenuItem();
			menuItem.Text = "User Management";
			
			menuItem.MenuItems = new List<MenuItem>();
			menuItem.MenuItems.Add(new MenuItem
			{
				Text = "Users",
				ActionName = "Index",
				Controller = "User",
				Area = "UserManagement",
				UserRoles = new string[]{UserControllerRoles.ViewUser.ToString(), UserControllerRoles.CreateUser.ToString()}
			});
			menuItem.MenuItems.Add(new MenuItem { Text = "Groups", ActionName = "Index", Controller = "Group", Area = "UserManagement" });
			menuItem.MenuItems.Add(new MenuItem { Text = "Logout", ActionName = "Logout", Controller = "User", Area = "UserManagement" });
			
			return menuItem;
		}

		public Tile GetTile()
		{
			return new Tile();
		}

        public Dictionary<string, string> MetaData
        {
            get
            {
                Dictionary<string, string> MetaData = new Dictionary<string, string>();
                MetaData.Add("Name", "UserManagement");
                MetaData.Add("Author", "Willem de Jong");

                return MetaData;
            }
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

		private List<CheckBoxListRole> LoadRoles(List<Role> allRoles, IQueryable<Role> selectedRoles)
		{
			List<CheckBoxListRole> userRoles = new List<CheckBoxListRole>();
			
			foreach (Role role in allRoles)
			{
				userRoles.Add(new CheckBoxListRole
				{
					Id = role.Id,
					Name = role.Name,
					Description = role.Description,
					Checked = (selectedRoles.FirstOrDefault<Role>(r => r.Id == role.Id) != null)
				});
			}

			return userRoles;

		}

		private List<CheckBoxListGroup> LoadGroups(List<Group> allGroups, IQueryable<Group> selectedGroups)
		{
			List<CheckBoxListGroup> userGroups = new List<CheckBoxListGroup>();
			
			foreach (Group group in allGroups)
			{
				userGroups.Add(new CheckBoxListGroup
				{
					Id = group.Id,
					Name = group.Name,
					Description = group.Description,
					Checked = (selectedGroups.FirstOrDefault<Group>(g => g.Id == group.Id) != null)
				});
			}

			return userGroups;
		}

		#endregion //private methods
	}
}
