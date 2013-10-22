﻿using System;
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
		[TestMethod]
		public void Install_GetFilesFromDirectory()
		{
			Mock<IDirectory> directory = new Mock<IDirectory>();

			directory.Setup(d => d.GetFiles(It.IsAny<string>(), It.IsAny<string>(), SearchOption.AllDirectories)
				).Returns(new string []{""});

			InstallController installController = new InstallController(directory.Object, "", "", new List<Package>());
			installController.Install();

			directory.Verify(d => d.GetFiles(It.IsAny<string>(), It.IsAny<string>(), SearchOption.AllDirectories));
		}
	}
}