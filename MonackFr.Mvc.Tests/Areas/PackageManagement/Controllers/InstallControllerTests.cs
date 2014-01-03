using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonackFr.Mvc.Areas.PackageManagement;
using MonackFr.Mvc.Areas.PackageManagement.Controllers;
using MonackFr.Mvc.Areas.PackageManagement.Entities;
using MonackFr.Mvc.Areas.PackageManagement.Repositories;
using Moq;
using System;
using System.Web.Mvc;

namespace MonackFr.Mvc.Tests.Areas.PackageManagement.Controllers
{
	[TestClass]
	public class InstallControllerTests
	{
        private Mock<IPackageManager> _packageManager = null;
        private Mock<IPackageRepository> _packageRepository = null;
        private Mock<IDatabaseManager> _databaseManager = null;
        private Mock<IUserManager> _userManager = null;

        private InstallController _installController = null;

        [TestInitialize()]
        public void Initialize()
        {
            _packageManager = new Mock<IPackageManager>();
            _packageRepository = new Mock<IPackageRepository>();
            _databaseManager = new Mock<IDatabaseManager>();
            _userManager = new Mock<IUserManager>();

            _installController = new InstallController(_packageManager.Object, _packageRepository.Object, _databaseManager.Object, _userManager.Object);
        }

        [TestMethod]
        public void Install_LoadsPackages()
        {    
            _installController.Install();
            _packageManager.Verify(p => p.LoadPackages(), Times.Exactly(1));
        }

        [TestMethod]
        public void Install_GetsPackages()
        {
            _installController.Install();
            _packageManager.Verify(p => p.Packages, Times.Exactly(1));
        }

        [TestMethod]
        public void Install_ReturnsViewWithNameInstall()
        {
            ViewResult viewResult = _installController.Install();
            Assert.AreEqual("install", viewResult.ViewName);            
        }

        [TestMethod]
        public void Install_WithPackages_LoadsPackages()
        {
            _installController.Install(new FormCollection());
            _packageManager.Verify(p => p.LoadPackages(), Times.Exactly(1));
        }
        
        [TestMethod]
        public void Install_WithPackages_InstallsPackages()
        {
            _installController.Install(new FormCollection());
            _packageRepository.Verify(p => p.InstallPackages(It.IsAny<IPackage[]>()), Times.Exactly(1));
        }

        [TestMethod]
        public void Install_WithPackages_CreatesUser()
        {
            _installController.Install(new FormCollection());
            _userManager.Verify(u => u.CreateUser(It.IsAny<string>(), It.IsAny<string>()));
        }

        [TestMethod]
        public void Install_WithPackages_AddsUserToAllRoles()
        {
            _installController.Install(new FormCollection());
            _userManager.Verify(u => u.AddUserToAllRoles(It.IsAny<string>()));
        }

        [TestMethod]
        public void Install_WithPackages_ReturnsRedirection()
        {
            RedirectToRouteResult result = _installController.Install(new FormCollection()) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Install", result.RouteValues["action"]);

        }
	}
}
