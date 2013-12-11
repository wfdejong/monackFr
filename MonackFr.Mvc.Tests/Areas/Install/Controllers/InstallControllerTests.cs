using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MonackFr.Wrappers;
using MonackFr.Mvc.Areas.Install.Controllers;
using System.IO;
using System.Collections.Generic;

namespace MonackFr.Mvc.Tests.Areas.Install.Controllers
{
	[TestClass]
	public class InstallControllerTests
	{
        //[TestMethod]
        //public void Install_GetFilesFromDirectory()
        //{
        //    Mock<IDirectory> directory = new Mock<IDirectory>();

        //    directory.Setup(d => d.GetFiles(It.IsAny<string>(), It.IsAny<string>(), SearchOption.AllDirectories)
        //        ).Returns(new string []{""});

        //    InstallController installController = new InstallController(directory.Object, "", "", new PackageManager());
        //    installController.Install();

        //    directory.Verify(d => d.GetFiles(It.IsAny<string>(), It.IsAny<string>(), SearchOption.AllDirectories));
        //}

        [TestMethod]
        public void Install_LoadsPackages()
        {
            Mock<IPackageManager> packageManager = new Mock<IPackageManager>();
            InstallController installController = new InstallController(new Wrappers.Directory(), "", "", packageManager.Object);
            installController.Install();
            packageManager.Verify(p => p.LoadPackages(It.IsAny<string>(), It.IsAny<string>()));
        }

        [TestMethod]
        public void Install_GetsPackages()
        {
        }
	}
}
