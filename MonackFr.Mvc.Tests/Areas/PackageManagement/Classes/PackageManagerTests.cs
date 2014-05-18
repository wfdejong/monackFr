using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MonackFr.Mvc.Areas.PackageManagement.Entities;
using System.Collections.Generic;
using MonackFr.Repository;
using MonackFr.Mvc.Module;
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
		private Mock<IList<IPackage>> _packages;
		private IPackageManager _packageManager;
		private Mock<IPackageManager> _mPackageManager;
		private Mock<IRoles> _roles;

		[TestInitialize]
		public void Initialize()
		{
			_directory = new Mock<IDirectory>();
			_packages = new Mock<IList<IPackage>>();
			_mPackageManager = new Mock<IPackageManager>();
			_roles = new Mock<IRoles>();
			_packageManager = new PackageManager(_directory.Object, _packages.Object, _mPackageManager.Object, _roles.Object);
		}

        [TestMethod]
		[ExpectedException(typeof(PackageNullReferenceException))]
        public void AddPackage_WithEmptyPackage_ThrowsException()
        {
            _packageManager.AddPackage(null);
        }

        [TestMethod]
        public void AddPackage_WithPackage_LoadsContexts()
        {
			Mock<IPackage> package = new Mock<IPackage>();
			_packageManager.AddPackage(package.Object);
			package.Verify(p => p.LoadContexts());
        }

        [TestMethod]
        public void AddPackage_WithPackage_LoadsModules()
        {
			Mock<IPackage> package = new Mock<IPackage>();
			_packageManager.AddPackage(package.Object);
			package.Verify(p => p.LoadModules());
        }

        [TestMethod]
        public void AddPackage_WithPackage_AddsPackage()
        {			
			Mock<IList<IContext>> contexts = new Mock<IList<IContext>>(); 
			contexts.Setup(c => c.Count).Returns(1);
			
			Mock<IList<IModule>> modules = new Mock<IList<IModule>>();
			modules.Setup(m => m.Count).Returns(1);

			Mock<IPackage> package = new Mock<IPackage>();			
			package.Setup(p => p.Contexts).Returns(contexts.Object);
			package.Setup(p => p.Modules).Returns(modules.Object);

			bool returnvalue = _packageManager.AddPackage(package.Object);
			_packages.Verify(p => p.Add(It.IsAny<IPackage>()));

        }

		[TestMethod]
		public void AddPackage_WithPackage_ReturnsTrue()
		{
			Mock<IList<IContext>> contexts = new Mock<IList<IContext>>();
			contexts.Setup(c => c.Count).Returns(1);

			Mock<IList<IModule>> modules = new Mock<IList<IModule>>();
			modules.Setup(m => m.Count).Returns(1);

			Mock<IPackage> package = new Mock<IPackage>();
			package.Setup(p => p.Contexts).Returns(contexts.Object);
			package.Setup(p => p.Modules).Returns(modules.Object);

			bool returnvalue = _packageManager.AddPackage(package.Object);
			
			Assert.IsTrue(returnvalue);
		}

        [TestMethod]
        public void AddPackage_WithPackageWithoutContexts_ReturnsFalse()
        {
			Mock<IList<IContext>> contexts = new Mock<IList<IContext>>();
			contexts.Setup(c => c.Count).Returns(0);

			Mock<IList<IModule>> modules = new Mock<IList<IModule>>();
			modules.Setup(m => m.Count).Returns(1);

			Mock<IPackage> package = new Mock<IPackage>();
			package.Setup(p => p.Contexts).Returns(contexts.Object);
			package.Setup(p => p.Modules).Returns(modules.Object);

			bool returnvalue = _packageManager.AddPackage(package.Object);

			Assert.IsFalse(returnvalue);
        }

		[TestMethod]
		public void AddPackage_WithPackageWithoutModules_ReturnsFalse()
		{
			Mock<IList<IContext>> contexts = new Mock<IList<IContext>>();
			contexts.Setup(c => c.Count).Returns(1);

			Mock<IList<IModule>> modules = new Mock<IList<IModule>>();
			modules.Setup(m => m.Count).Returns(0);

			Mock<IPackage> package = new Mock<IPackage>();
			package.Setup(p => p.Contexts).Returns(contexts.Object);
			package.Setup(p => p.Modules).Returns(modules.Object);

			bool returnvalue = _packageManager.AddPackage(package.Object);

			Assert.IsFalse(returnvalue);
		}

        [TestMethod]
        public void LoadPackages_AddsPackage()
        {
			_directory.Setup(d => d.GetFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<System.IO.SearchOption>())).Returns(new string[]{"test"});
			_mPackageManager.Setup(p => p.BaseDirectory).Returns("test");

			_packageManager.LoadPackages();
			_mPackageManager.Verify(p => p.AddPackage(It.IsAny<IPackage>()));
        }

        [TestMethod]
        public void Authorizations_LoadsAuthorizations()
        {
			Mock<IPackage> package = new Mock<IPackage>();
			_mPackageManager.Setup(p => p.Packages).Returns(new IPackage[] { package.Object });
			Security.IAuthorization[] authorizations = _packageManager.Authorizations;
			package.Verify(p => p.LoadAuthorizations());
        }

        [TestMethod]
        public void Authorizations_WithNoAuthorizations_ReturnsEmptyArray()
        {
			Mock<IPackage> package = new Mock<IPackage>();
			_mPackageManager.Setup(p => p.Packages).Returns(new IPackage[] { package.Object });
			package.Setup(p => p.Authorizations).Returns((List<Security.IAuthorization>)null);
			IAuthorization[] authorizations = _packageManager.Authorizations;
			Assert.AreEqual(authorizations.Length, 0);
        }
        
        [TestMethod]
        public void Authorizations_WithAuthorizations_AddsAuthorizationsToArray()
        {
			Mock<IPackage> package = new Mock<IPackage>();
			Mock<IAuthorization> authorization = new Mock<IAuthorization>();
			_mPackageManager.Setup(p => p.Packages).Returns(new IPackage[] { package.Object });
			package.Setup(p => p.Authorizations).Returns(new List<Security.IAuthorization>(){ authorization.Object });
			IAuthorization[] authorizations = _packageManager.Authorizations;
			Assert.AreEqual(authorizations.Length, 1);
			Assert.AreEqual(authorizations[0], authorization.Object);
        }
        
        [TestMethod]
        public void InstallRoles_GetsRoles()
        {
			Mock<IAuthorization> authorization = new Mock<IAuthorization>();
			Mock<IMfrRole> role = new Mock<IMfrRole>();

			authorization.Setup(a => a.GetRoles()).Returns(new List<IMfrRole>(){ role.Object });
			_mPackageManager.Setup(p => p.Authorizations).Returns(new IAuthorization[] { authorization.Object });
			_packageManager.InstallRoles();
			authorization.Verify(a => a.GetRoles());			
        }
		
        [TestMethod]
        public void InstallRoles_WithoutMfrRoleProvider_CreatesRole()
        {
			Mock<IAuthorization> authorization = new Mock<IAuthorization>();
			Mock<IMfrRole> role = new Mock<IMfrRole>();

			authorization.Setup(a => a.GetRoles()).Returns(new List<IMfrRole>() { role.Object });
			_mPackageManager.Setup(p => p.Authorizations).Returns(new IAuthorization[] { authorization.Object });
			_packageManager.InstallRoles();
			_roles.Verify(r => r.CreateRole(It.IsAny<IMfrRole>()));
        }
    }
}
