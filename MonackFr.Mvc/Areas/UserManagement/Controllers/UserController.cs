using MonackFr.Repository;
using MonackFr.Mvc.Areas.UserManagement.Providers;
using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using MonackFr.Mvc.Areas.UserManagement.Package;
using AutoMapper;
using MonackFr.Mvc.JqueryUiHelpers;

namespace MonackFr.Mvc.Areas.UserManagement.Controllers
{	
	public class UserController : DisposeController
	{
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
			: this(new MonackFr.Mvc.Repositories.UserRepository(), new Authentication())
		{			
		}

		/// <summary>
		/// Constructor with custom repository and authentication object
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="authentication"></param>
		public UserController(MonackFr.Mvc.Repositories.IUserRepository repository, IAuthentication authentication)			
			:base((IDisposable)repository)
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
			return View();
		}

		public JsonResult GetUsers()
		{
			IEnumerable<ViewModels.User> users = Mapper.Map<IEnumerable<ViewModels.User>>(_repository.GetAll().ToArray());
			return DataTable.DataToJson<ViewModels.User>(users, u => u.Id);
		}

		//
		// GET: /User/Create
		public ActionResult Create()
		{
			return View();
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

			//detailsUser.Fill(user);
			detailsUser.UserRoles = LoadRoles(roles, user.Roles.AsQueryable<Entities.Role>()); //automapper

			detailsUser.UserGroups = LoadGroups(groups, user.Groups.AsQueryable<Entities.Group>()); //automapper
			
			return View(detailsUser);
		}

		public ViewResult Details()
		{
			return View();
		}
		

		/// <summary>
		/// Creates a user
		/// </summary>
		/// <param name="createUser"></param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult Create(ViewModels.CreateUser createUser)
		{
			if (ModelState.IsValid)
			{	
				Membership.CreateUser(createUser.UserName, createUser.Password, createUser.Email);
				return Json(new { success = true });
			}
			else
			{
				return Json(new { success = false });
			}
		}

		/// <summary>
		/// Deletes a user
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult Delete(string username)
		{
			Membership.DeleteUser(username);
			return Json(new { success = true });
		}

		//
		// GET: /User/Edit/5
		public ViewResult Edit(int id)
		{
			Entities.User user = _repository.GetSingle(u => u.Id == id);

			ViewModels.DetailsUser detailsUser = new ViewModels.DetailsUser();
			//detailsUser.Fill(user);

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
				//detailsUser.Map(user);

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

		[HttpPost]
		public ActionResult AddRole(int userId, int allGroups)
		{
			Entities.User user = _repository.GetSingle(u => u.Id == userId);
			return View(user);
		}

		#endregion //public methods

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
