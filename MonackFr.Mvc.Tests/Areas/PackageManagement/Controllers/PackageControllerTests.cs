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

namespace MonackFr.Mvc.Tests.Areas.PackageManagement.Controllers
{
	[TestClass]
	public class PackageControllerTests
	{
        private Mock<IPackageManager> _packageManager = null;
        private Mock<IPackageRepository> _packageRepository = null;

        private PackageController _packageController = null;

        [TestInitialize]
        public void Initialize()
        {
            _packageManager = new Mock<IPackageManager>();
            _packageRepository = new Mock<IPackageRepository>();
            _packageRepository.As<IDisposable>();
            _packageController = new PackageController(_packageRepository.Object, _packageManager.Object);
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
			throw new NotImplementedException();
		}

		[TestMethod]
		[ExpectedException(typeof(FileNotFoundException))]
		public void InstallPackage_WithoutPath_ThrowsException()
		{
			throw new NotImplementedException();
		}		

		[TestMethod]
		public void InstallPackage_WithPath_CreatesPackage()
		{
			throw new NotImplementedException();
		}

		[TestMethod]
		public void InstallPackage_WithPath_SavesPackage()
		{
			throw new NotImplementedException();
		}

		[TestMethod]
		public void InstallPackage_WithPath_Redirects()
		{
			throw new NotImplementedException();
		}
		
		[TestMethod]
		[ExpectedException(typeof(FileNotFoundException))]
		public void RemovePackage_WithoutPath_ThrowsException()
		{
			throw new NotImplementedException();
		}

		[TestMethod]
		public void RemovePackage_WithPath_DeletesPackage()
		{
			throw new NotImplementedException();
		}

		[TestMethod]
		public void RemovePackage_WithPath_SavesDeletion()
		{
			throw new NotImplementedException();
		}

		[TestMethod]
		public void RemovePackage_WithPath_Redirects()
		{
			throw new NotImplementedException();
		}

	}
}
