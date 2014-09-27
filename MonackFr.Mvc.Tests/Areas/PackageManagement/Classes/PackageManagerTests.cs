using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MonackFr.Mvc.Areas.PackageManagement.Entities;
using System.Collections.Generic;
using MonackFr.Repository;
using MonackFr.Wrappers;
using MonackFr.Mvc.Areas.PackageManagement;
using MonackFr.Mvc.Contexts;
using MonackFr.Security;
using System.Web.Security;

namespace MonackFr.Mvc.Tests.Areas.PackageManagement.Classes
{
	[TestClass]
	public class PackageManagerTests
	{
		private Mock<IDirectory> _directory;
		private IPackageManager _packageManager;
		private Mock<IPackageManager> _mPackageManager;
		
		[TestInitialize]
		public void Initialize()
		{
			_directory = new Mock<IDirectory>();
			_mPackageManager = new Mock<IPackageManager>();
			_packageManager = new PackageManager(_directory.Object, _mPackageManager.Object);
		}

		[TestMethod]
		public void GetPackage_WithOutIPackage_ReturnsNull()
		{
			//To test this, the loadedPackage variable in GetPackage needs to be refoactored into
			// a class with a parameter less constructor so it can be mocked. Consider chain class for easy use.
			throw new NotImplementedException();
		}

		[TestMethod]
		public void GetPackage_WithIPackage_ReturnsPackage()
		{
			throw new NotImplementedException();
		}

		[TestMethod]
		public void GetPackage_WithIPackageAndModule_ReturnsPackageWithModule()
		{
			throw new NotImplementedException();
		}

		[TestMethod]
		public void GetPackage_WithIPackageAndContext_ReturnsPackageWithContext()
		{
			throw new NotImplementedException();
		}

		[TestMethod]
		public void GetPackage_WithIPackageModuleAndAuthorizations_ReturnsModuleWithRoles()
		{
			throw new NotImplementedException();
		}

		[TestMethod]
		public void GetPackages_GetsFilesFromDirectory()
		{
			throw new NotImplementedException();
		}

		[TestMethod]
		public void GetPackages_GetsPackage()
		{
			throw new NotImplementedException();
		}

		[TestMethod]
		public void GetPackages_AddsPackage()
		{
			throw new NotImplementedException();
		}
	}
}
