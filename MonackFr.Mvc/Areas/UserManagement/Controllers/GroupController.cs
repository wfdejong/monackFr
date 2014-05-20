using MonackFr.Mvc.Areas.UserManagement.ViewModels;
using MonackFr.Mvc.Entities;
using MonackFr.Mvc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MonackFr.Mvc.Areas.UserManagement.Controllers
{
	public class GroupController : BaseController
	{
		#region private properties

		private IGroupRepository _repository;

		#endregion //private properties

		#region constructors

		public GroupController()
		{
			_repository = new GroupRepository();
		}

		public GroupController(IGroupRepository repository)
		{
			_repository = repository;
		}

		#endregion //constructors

		//
		// GET: /Group/

		public ActionResult Index()
		{
			return View(_repository.GetAll());
		}

		//
		// GET: /Group/Details/5

		public ActionResult Details(int id)
		{
			Group group = _repository.GetSingle(g => g.Id == id);
			DetailsGroup detailsGroup = new DetailsGroup();
			detailsGroup.Fill(group);

			IRoleRepository roleRepository = new RoleRepository();
			
			List<Role> roles = roleRepository.GetAll().ToList<Role>();
			detailsGroup.GroupRoles = LoadRoles(roles, group.Roles.ToList<Role>());
			detailsGroup.Users = group.Users.ToList();
			
			return View(detailsGroup);
		}

		//
		// GET: /Group/Create

		public ActionResult Create()
		{
			return View();
		}

		//
		// POST: /Group/Create

		[HttpPost]
		public ActionResult Create(Group group)
		{
			if (ModelState.IsValid)
			{
				//Group group = createGroup.Map();
				_repository.Create(group);
				_repository.Save();
				return RedirectToAction("Index");
			}
			else
			{
				return View(group);
			}
		}

		//
		// GET: /Group/Edit/5

		public ActionResult Edit(int id)
		{
			Group group = _repository.GetSingle(g => g.Id == id);
			DetailsGroup detailsGroup = new DetailsGroup();
			detailsGroup.Fill(group);

			IRoleRepository roleRepository = new RoleRepository();
			List<Role> roles = roleRepository.GetAll().ToList<Role>();
			detailsGroup.GroupRoles = LoadRoles(roles, group.Roles.ToList<Role>());
			detailsGroup.Users = group.Users.ToList<User>();

			IUserRepository userRepository = new UserRepository();
			ViewBag.AllUsers = new SelectList(userRepository.GetAll(), "Id", "UserName");

			return View(detailsGroup);
		}

		//
		// POST: /Group/Edit/5

		[HttpPost]
		public ActionResult Edit(DetailsGroup detailsGroup)
		{
			if (ModelState.IsValid)
			{
				Group group = _repository.GetSingle(g => g.Id == detailsGroup.Id);
				detailsGroup.Map(group);

				//Remove all roles from group
				_repository.RemoveAllRolesFromGroup(group);

				//Add roles to group
				foreach (CheckBoxListRole groupRole in detailsGroup.GroupRoles)
				{
					if (groupRole.Checked)
					{
						Role role = new Role() { Id = groupRole.Id };
						_repository.AddRolesToGroup(group, role);
					}
				}

				_repository.Edit(group);
				_repository.Save();
				return RedirectToAction("Details", detailsGroup);
			}
			else
			{
				return View();
			}
		}
			   
		//
		// POST: /Group/Delete/5

		[HttpPost]
		public ActionResult Delete(Int32 id)
		{
			Group group = _repository.GetSingle(g => g.Id == id);
			_repository.Delete(group);
			_repository.Save();
			return RedirectToAction("Index");
		}

		//
		// Post /Group/AddUserToGroup
		[HttpPost]
		public ActionResult AddUserToGroup(FormCollection collection)
		{
			int groupId = Convert.ToInt32(collection["Id"]);
			int userId = Convert.ToInt32(collection["userId"]);
			_repository.AddUserToGroup(groupId, userId);
			return RedirectToAction("Edit", new { Id = groupId });
		}

		[HttpPost]
		public ActionResult RemoveUserFromGroup(int userId, int groupId)
		{
			_repository.RemoveUserFromGroup(groupId, userId);

			return RedirectToAction("Edit", new {Id = groupId});
		}

		private List<CheckBoxListRole> LoadRoles(List<Role> allRoles, List<Role> selectedRoles)
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
	}
}
