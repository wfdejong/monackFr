using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace MonackFr.Mvc.Tests.Areas.PackageManagement.Controllers
{
	[TestClass]
	public class PackageControllerTests
	{
		[TestMethod]
		public void Index_LoadsPackages()
		{
			throw new NotImplementedException();
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
