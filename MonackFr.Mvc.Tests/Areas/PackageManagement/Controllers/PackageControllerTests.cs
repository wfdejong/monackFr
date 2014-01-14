using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MonackFr.Mvc.Areas.PackageManagement.Controllers;
using MonackFr.Mvc.Areas.PackageManagement;
using MonackFr.Mvc.Areas.PackageManagement.Repositories;
using Moq;
using System.Collections;
using MonackFr.Mvc.Areas.PackageManagement.Entities;
using System.Collections.Generic;
using System.Linq;
using MonackFr.Wrappers;
using System.Web.Mvc;

namespace MonackFr.Mvc.Tests.Areas.PackageManagement.Controllers
{
	[TestClass]
	public class PackageControllerTests
	{
        private Mock<IPackageManager> _packageManager = null;
        private Mock<IPackageRepository> _packageRepository = null;
        private Mock<IFile> _file = null;

        private PackageController _packageController = null;

        [TestInitialize]
        public void Initialize()
        {
            _packageManager = new Mock<IPackageManager>();
            _packageRepository = new Mock<IPackageRepository>();
            _packageRepository.As<IDisposable>();
            _file = new Mock<IFile>();
            _packageController = new PackageController(_packageRepository.Object, _packageManager.Object, _file.Object);
        }

		[TestMethod]
		public void Index_LoadsPackages()
        {
            _packageRepository.Setup(p => p.GetAll()).Returns(new List<Package>() { }.AsQueryable());

            _packageController.Index();
            _packageManager.Verify(p => p.LoadPackages());
		}

		[TestMethod]		
		public void Index_ReturnsView()
		{
			_packageRepository.Setup(p => p.GetAll()).Returns(new List<Package>() { }.AsQueryable());

			ViewResult view = (ViewResult)_packageController.Index();
		}

		[TestMethod]
		[ExpectedException(typeof(FileNotFoundException))]
		public void InstallPackage_WithoutPath_ThrowsException()
		{
			_file.Setup(f => f.Exists(It.IsAny<string>())).Returns(false);
			_packageController.InstallPackage(null);
		}		

		[TestMethod]
		public void InstallPackage_WithPath_CreatesPackage()
		{
			_file.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
			_packageController.InstallPackage("");
			_packageRepository.Verify(p => p.Create(It.IsAny<Package>()));
		}

		[TestMethod]
		public void InstallPackage_WithPath_SavesPackage()
		{
			_file.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
			_packageController.InstallPackage("");
			_packageRepository.Verify(p => p.Save());
		}

		[TestMethod]
		public void InstallPackage_WithPath_Redirects()
		{
			_file.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);

			RedirectToRouteResult result = (RedirectToRouteResult)_packageController.InstallPackage("");

			Assert.IsNotNull(result);
			Assert.AreEqual("index", result.RouteValues["action"]);
		}
		
		[TestMethod]
		[ExpectedException(typeof(FileNotFoundException))]
		public void DeletePackage_WithoutPath_ThrowsException()
		{
            _file.Setup(f => f.Exists(It.IsAny<string>())).Returns(false);
            _packageController.DeletePackage(null);
		}

		[TestMethod]
		public void DeletePackage_WithPath_DeletesPackage()
		{
            _file.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
            _packageController.DeletePackage("");
            _packageRepository.Verify(p => p.Delete(It.IsAny<Package>()));
		}

		[TestMethod]
		public void DeletePackage_WithPath_SavesDeletion()
		{
            _file.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
            _packageController.DeletePackage("");
            _packageRepository.Verify(p => p.Save());
		}

		[TestMethod]
		public void DeletePackage_WithPath_Redirects()
		{

            _file.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
            RedirectToRouteResult result = (RedirectToRouteResult)_packageController.DeletePackage("");

            Assert.IsNotNull(result);
            Assert.AreEqual("index", result.RouteValues["action"]);
		}

	}
}
