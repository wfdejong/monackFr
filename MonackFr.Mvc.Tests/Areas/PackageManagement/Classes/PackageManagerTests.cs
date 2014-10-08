using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using MonackFr.Repository;
using MonackFr.Wrappers;
using MonackFr.Mvc.Contexts;
using MonackFr.Security;
using System.Web.Security;
using System.Linq;
using MonackFr.Module;
using AutoMapper;
using TestedPackageManagement = MonackFr.Mvc.Areas.PackageManagement;

namespace MonackFr.Mvc.Tests.Areas.PackageManagement.Classes
{
	[TestClass]
	public class PackageManagerTests
	{
		private Mock<IDirectory> _directory;
		private TestedPackageManagement.IPackageManager _packageManager;
		private Mock<TestedPackageManagement.IPackageManager> _packageManagerMock;
		private Mock<ILoader<IPackage>> _packageLoader;
		private Mock<ILoader<IModule>> _moduleLoader;
		private Mock<ILoader<IContext>> _contextLoader;
		private Mock<IPackage> _package;
		private Mock<IContext> _context;
		private Mock<IModule> _module;
		private Mock<IAuthorization> _authorization;
		private Mock<IMfrRole> _role;
		private Mock<IMappingEngine> _mapper;
		TestedPackageManagement.Package _mappedPackage;
		TestedPackageManagement.Module _mappedModule;
		
		[TestInitialize]
		public void Initialize()
		{
			_directory = new Mock<IDirectory>();
			_packageManagerMock = new Mock<TestedPackageManagement.IPackageManager>();
			_moduleLoader = new Mock<ILoader<IModule>>();
			_contextLoader = new Mock<ILoader<IContext>>();
			_packageLoader = new Mock<ILoader<IPackage>>();
			_package = new Mock<IPackage>();
			_context = new Mock<IContext>();
			_mapper = new Mock<IMappingEngine>();
			_module = new Mock<IModule>();
			_authorization = _module.As<IAuthorization>();
			_role = new Mock<IMfrRole>();
			
			_packageManager = new TestedPackageManagement.PackageManager(_directory.Object, _packageManagerMock.Object, _moduleLoader.Object, _contextLoader.Object, _packageLoader.Object, _mapper.Object);

			_directory.Setup(d => d.GetFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<System.IO.SearchOption>())).Returns(new string[] { "", "" });
			
			_packageLoader.SetupGet(p => p.LoadedItems).Returns(new List<IPackage>() { _package.Object });
			_contextLoader.SetupGet(c => c.LoadedItems).Returns(new List<IContext>() { _context.Object });
			_moduleLoader.SetupGet(m => m.LoadedItems).Returns(new List<IModule>() { _module.Object });

			_packageLoader.Setup(p => p.Load(It.IsAny<string>())).Returns(_packageLoader.Object);
			_contextLoader.Setup(c => c.Load(It.IsAny<string>())).Returns(_contextLoader.Object);
			_moduleLoader.Setup(p => p.Load(It.IsAny<string>())).Returns(_moduleLoader.Object);

			_authorization.Setup(a => a.GetRoles()).Returns(new List<IMfrRole>() { _role.Object });
			
			_packageManagerMock.Setup(p => p.BaseDirectory).Returns("1");

			_mappedPackage = new TestedPackageManagement.Package();
			_mappedModule = new TestedPackageManagement.Module();
			_mapper.Setup(m => m.Map<TestedPackageManagement.Package>(It.IsAny<IPackage>())).Returns(_mappedPackage);
			_mapper.Setup(m => m.Map<TestedPackageManagement.Module>(It.IsAny<IModule>())).Returns(_mappedModule);

			_packageManagerMock.Setup(p => p.GetPackage(It.IsAny<string>())).Returns(_mappedPackage);
		}

		[TestMethod]
		public void GetPackage_WithOutIPackage_ReturnsNull()
		{
			_packageLoader.SetupGet(p => p.LoadedItems).Returns(new List<IPackage>());
			_packageLoader.Setup(p => p.Load(It.IsAny<string>())).Returns(_packageLoader.Object);
			_packageManagerMock.Setup(p => p.BaseDirectory).Returns("1");
			Assert.IsNull(_packageManager.GetPackage("2"));			
		}

		[TestMethod]
		public void GetPackage_WithIPackage_ReturnsPackage()
		{	
			Assert.AreEqual(_mappedPackage, _packageManager.GetPackage("2"));
		}

		[TestMethod]
		public void GetPackage_WithIPackage_GetsLoadedPackages()
		{
			_packageManager.GetPackage("2");
			_packageLoader.VerifyGet(p => p.LoadedItems, Times.AtLeastOnce());
		}

		[TestMethod]
		public void GetPackage_WithIPackage_GetsLoadedModules()
		{			
			_packageManager.GetPackage("2");
			_moduleLoader.VerifyGet(m => m.LoadedItems, Times.AtLeastOnce());
		}

		[TestMethod]
		public void GetPackage_WithIPackage_GetsLoadedContexts()
		{
			_packageManager.GetPackage("2");
			_contextLoader.VerifyGet(c => c.LoadedItems, Times.AtLeastOnce());
		}

		[TestMethod]
		public void GetPackage_WithIPackage_GetsLoadedAuthorizations()
		{ 
			_packageManager.GetPackage("2");
			_authorization.Verify(a => a.GetRoles(), Times.AtLeastOnce());
		}


		[TestMethod]
		public void GetPackage_WithIPackageAndModule_ReturnsPackageWithModule()
		{
			Assert.AreSame(_mappedModule, _packageManager.GetPackage("2").Modules.First());
		}

		[TestMethod]
		public void GetPackage_WithIPackageAndContext_ReturnsPackageWithContext()
		{
			Assert.AreSame(_context.Object, _packageManager.GetPackage("2").Contexts.First());
		}

		[TestMethod]
		public void GetPackage_WithIPackageModuleAndAuthorizations_ReturnsModuleWithRoles()
		{
			Assert.AreSame(_role.Object, _packageManager.GetPackage("2").Modules.First().Roles.First());
		}

		[TestMethod]
		public void GetPackages_GetsFilesFromDirectory()
		{
			_packageManager.GetPackages();
			_directory.Verify(d => d.GetFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<System.IO.SearchOption>()), Times.Once());
		}

		[TestMethod]
		public void GetPackages_ReadsPackageDirectory()
		{
			_packageManager.GetPackages();
			_packageManagerMock.VerifyGet(p => p.PackageDirectory, Times.Once());
			
		}

		[TestMethod]
		public void GetPackages_GetsPackage()
		{
			_packageManager.GetPackages();
			_packageManagerMock.Verify(p => p.GetPackage(It.IsAny<string>()), Times.Exactly(2));
		}

		[TestMethod]
		public void GetPackages_ReturnsPackage()
		{
			IEnumerable<TestedPackageManagement.Package> packages = _packageManager.GetPackages();
			Assert.AreSame(packages.First(), _mappedPackage);
		}
	}
}
