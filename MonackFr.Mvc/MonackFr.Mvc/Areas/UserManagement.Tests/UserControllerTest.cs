using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonackFr.Mvc.Areas.UserManagement.Controllers;
using MonackFr.Mvc.Areas.UserManagement.Models;
using MonackFr.Mvc.Areas.UserManagement.ViewModels;

namespace MonackFr.Mvc.Areas.UserManagement.Tests
{
	/// <summary>
	/// Test class for UserController
	/// </summary>
	[TestClass]
	public class UserControllerTest
	{
		/// <summary>
		///A test for Index
		///</summary>
		[TestMethod()]
		public void UserIndexTest()
		{
			UserController controller = new UserController(new TestUserRepository(), new TestAuthentication());
			ActionResult actionResult = controller.Index();
			Assert.IsNotNull(actionResult);
		}

		/// <summary>
		///Test if login view is returned
		///</summary>
		[TestMethod()]
		public void UserViewLoginTest()
		{
			UserController controller = new UserController();
			ViewResult viewResult = controller.Login();
			Assert.IsNotNull(viewResult);
		}

		/// <summary>
		/// Test if user can login successfully
		/// </summary>
		[TestMethod()]
		public void UserLoginTest()
		{
			UserController controller = new UserController(new TestUserRepository(), new TestAuthentication());

			CreateUser createUser = new CreateUser { UserName = "UnitTestUserName", Password = "TestPassword", RetypePassword = "TestPassword" };
			User user = createUser.Map();			
			ActionResult createActionResult = controller.Create(createUser);

			LoginUser loginUser = new LoginUser { UserName = "UnitTestUserName", Password = "TestPassword" };
			ActionResult loginActionResult = controller.Login(loginUser, "");

			Assert.IsNotNull(loginActionResult);
		}

		/// <summary>
		/// Test if user with wrong combination fails to login
		/// </summary>
		[TestMethod()]
		public void UserLoginFailedTest()
		{
			Assert.Fail();
		}

		/// <summary>
		///Test if details view is returned
		///</summary>
		[TestMethod()]
		public void UserDetailsTest()
		{
			IUserRepository repository = new TestUserRepository();
			UserController controller = new UserController(repository, new TestAuthentication());
			User user = repository.GetAll().First<User>();

			ViewResult viewResult = controller.Details(user.Id);

			Assert.IsNotNull(viewResult);
			Assert.AreEqual(user, viewResult.ViewData.Model);
		}

		/// <summary>
		///Test if a view is returned
		///</summary>
		[TestMethod()]
		public void UserViewCreateTest()
		{
			UserController controller = new UserController(new TestUserRepository(), new TestAuthentication());
			ActionResult actionResult = controller.Create();
			Assert.IsNotNull(actionResult);
		}

		/// <summary>
		///Test if a new user is created
		///</summary>
		[TestMethod()]
		public void UserCreateTest()
		{
			TestUserRepository repository = new TestUserRepository();
			UserController controller = new UserController(repository, new TestAuthentication());
			CreateUser createUser = new CreateUser { UserName = "UnitTestUserName", Password="TestPassword", RetypePassword="TestPassword"};
			User user = createUser.Map();
			ActionResult actionResult = controller.Create(createUser);
			User returnedUser = repository.FindBy(u => u.UserName == "UnitTestUserName").First<User>();
			
			Assert.IsNotNull(actionResult);
			Assert.AreSame(user.UserName, returnedUser.UserName);
			Assert.IsTrue(repository.SaveMethodHit);
		}
				
		/// <summary>
		///Test if edit view is returned
		///</summary>
		[TestMethod()]
		public void UserViewEditTest()
		{
			IUserRepository repository = new TestUserRepository();
			UserController controller = new UserController(repository, new TestAuthentication());

			User editUser = repository.GetAll().First<User>();

			ViewResult viewResult = controller.Edit(editUser.Id);
			Assert.IsNotNull(viewResult);
			Assert.AreEqual(editUser, viewResult.ViewData.Model);		
		}

		/// <summary>
		///Test if user is edited
		///</summary>
		[TestMethod()]
		public void UserEditTest()
		{
			TestUserRepository repository = new TestUserRepository();
			UserController controller = new UserController(repository, new TestAuthentication());

			User oldUser = repository.GetAll().First<User>();
			//User newUser = new User { Id = oldUser.Id, CreationDate = DateTime.Now.AddDays(1), LastUpdate = DateTime.Now.AddDays(2), Username = "NewUsername" };
			//TODO create a user with constructor.
			Assert.Fail();
			/*controller.Edit(newUser);

			User editedUser = repository.FindBy(u => u.Id == oldUser.Id).First<User>();

			Assert.IsNotNull(editedUser);
			Assert.AreEqual(editedUser, newUser);
			Assert.IsTrue(repository.SaveMethodHit);*/
		}
		
		/// <summary>
		///Test if user is deleted
		///</summary>
		[TestMethod()]
		public void UserDeleteTest()
		{
			TestUserRepository repository = new TestUserRepository();
			UserController controller = new UserController(repository, new TestAuthentication());
			User user = repository.GetAll().First<User>();

			ActionResult viewResult = controller.Delete(user.Id);
			User deletedUser = repository.GetSingle(u => u.Id == user.Id);
			
			Assert.IsNotNull(viewResult);
			Assert.IsNull(deletedUser);
			Assert.IsTrue(repository.SaveMethodHit);
		}		
	}
}
