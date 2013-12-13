using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonackFr.Mvc.Areas.PackageManagement;
using MonackFr.Mvc.Areas.PackageManagement.Controllers;
using Moq;

namespace MonackFr.Mvc.Tests.Areas.PackageManagement.Controllers
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
            InstallController installController = new InstallController(packageManager.Object);
            installController.Install();
            packageManager.Verify(p => p.LoadPackages());
        }

        [TestMethod]
        public void Install_GetsPackages()
        {
            Mock<IPackageManager> packageManager = new Mock<IPackageManager>();
            InstallController installController = new InstallController(packageManager.Object);
            installController.Install();
            packageManager.Verify(p => p.Packages);
        }
	}
}
