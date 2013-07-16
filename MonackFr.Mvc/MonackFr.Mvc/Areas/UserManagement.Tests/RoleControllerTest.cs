using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonackFr.Mvc.Areas.UserManagement.Controllers;
using MonackFr.Mvc.Areas.UserManagement.Models;
using MonackFr.Mvc.Areas.UserManagement.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using MonackFr.Mvc.Areas.UserManagement;

namespace MonackFr.Mvc.Areas.UserManagement.Tests
{
	/// <summary>
	/// Test class for UserController
	/// </summary>
	[TestClass]
	public class RoleControllerTest
	{	
		/// <summary>
		/// Test if a view is returned
		/// </summary>
		[TestMethod]
		public void RoleViewIndexTest()
		{
			RoleController controller = new RoleController(new TestRoleRepository());
			ActionResult actionResult = controller.Index();
			Assert.IsNotNull(actionResult);
		}

		/// <summary>
		///A test for Details
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://mvc/test3")]
		public void DetailsTest()
		{
			RoleController target = new RoleController(); // TODO: Initialize to an appropriate value
			int id = 0; // TODO: Initialize to an appropriate value
			ActionResult expected = null; // TODO: Initialize to an appropriate value
			ActionResult actual;
			actual = target.Details(id);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}	

		/// <summary>
		///Test if a view is returned
		///</summary>
		[TestMethod()]
		public void RoleViewCreateTest()
		{
			RoleController controller = new RoleController(new TestRoleRepository());
			ActionResult actionResult = controller.Create();
			Assert.IsNotNull(actionResult);
		}

		/// <summary>
		/// Test if a new role is created
		/// </summary>
		[TestMethod]
		public void RoleCreateTest()
		{
			TestRoleRepository repository = new TestRoleRepository();
			RoleController controller = new RoleController(repository);
			Role role = new Role { Name = "UnitTestRoleName", Description="Unit test description" };
			ActionResult actionResult = controller.Create(role);
			Role returnedRole = repository.FindBy(u => u.Name == "UnitTestRoleName").First<Role>();

			Assert.IsNotNull(actionResult);
			Assert.AreSame(role.Name, returnedRole.Name);
			Assert.AreSame(role.Description, returnedRole.Description);
			Assert.IsTrue(repository.SaveMethodHit);
		}

		/// <summary>
		/// Test if correct view is returned
		/// </summary>
		[TestMethod]
		public void RoleViewEditTest()
		{
			IRoleRepository repository = new TestRoleRepository();
			RoleController controller = new RoleController(repository);

			Role editRole = repository.GetAll().First<Role>();

			ViewResult viewResult = controller.Edit(editRole.Id);
			Assert.IsNotNull(viewResult);
			Assert.AreEqual(editRole, viewResult.ViewData.Model);				
		}

		/// <summary>
		/// Test if a role is edited
		/// </summary>
		[TestMethod]
		public void RoleEditTest()
		{
			TestRoleRepository repository = new TestRoleRepository();
			RoleController controller = new RoleController(repository);

			Role oldRole = repository.GetAll().First<Role>();
			Role newRole = new Role { Id = oldRole.Id, Creation = DateTime.Now.AddDays(1), LastUpdate = DateTime.Now.AddDays(2), Name = "NewRolename", Description= "New Role Description" };

			controller.Edit(newRole);

			Role editedRole = repository.FindBy(r => r.Id == oldRole.Id).First<Role>();

			Assert.IsNotNull(editedRole);
			Assert.AreEqual(editedRole, newRole);
			Assert.IsTrue(repository.SaveMethodHit);
		}

		/// <summary>
		///A test for Delete
		///</summary>
		// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
		// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
		// whether you are testing a page, web service, or a WCF service.
		[TestMethod()]
		[HostType("ASP.NET")]
		[UrlToTest("http://mvc/test3")]
		public void DeleteTest()
		{
			RoleController target = new RoleController(); // TODO: Initialize to an appropriate value
			int id = 0; // TODO: Initialize to an appropriate value
			ActionResult expected = null; // TODO: Initialize to an appropriate value
			ActionResult actual;
			actual = target.Delete(id);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}			
	}
}
