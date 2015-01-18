using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using MonackFr.Repository;
using Moq;

namespace MonackFr.Library.Tests
{
	[TestClass]
	public class ModuleKeeperTests
	{
		[TestMethod]
		public void Add_WithOneModule_IncreasesNumberOfPluginsWithOne()
		{
			int pluginCount = ModuleKeeper.Instance.Modules.Count();
			Mock<IModule> iModule = new Mock<IModule>();
			iModule.Setup(m => m.SystemName).Returns("SystemName");
			ModuleKeeper.Instance.Add(iModule.Object);
			Assert.IsTrue(ModuleKeeper.Instance.Modules.Count() == pluginCount + 1);			
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Add_WithExistingSystemName_ThrowsException()
		{
			Mock<IModule> iModule = new Mock<IModule>();
			iModule.Setup(m => m.SystemName).Returns("SystemName");
			ModuleKeeper.Instance.Add(iModule.Object);

			Mock<IModule> iModule2 = new Mock<IModule>();
			iModule2.Setup(m => m.SystemName).Returns("SystemName");
			ModuleKeeper.Instance.Add(iModule2.Object);
		}

		[TestMethod]
		public void AddRange_WithMultipleModules_IncreasesNumberOfModules()
		{
			Mock<IModule> iModule1 = new Mock<IModule>();
			Mock<IModule> iModule2 = new Mock<IModule>();

			iModule1.Setup(m => m.SystemName).Returns("Module1");
			iModule2.Setup(m => m.SystemName).Returns("Module2");

			List<IModule> range = new List<IModule>()
				{
					iModule1.Object,
					iModule2.Object
				};
			int pluginCount = ModuleKeeper.Instance.Modules.Count();
			ModuleKeeper.Instance.AddRange(range);
			Assert.IsTrue(ModuleKeeper.Instance.Modules.Count() == pluginCount + 2);
		}

		[TestMethod]
		public void Dispose_DisposePluginLoader_RemovesAllPlugins()
		{
			if (ModuleKeeper.Instance.Modules.Count() == 0)
			{
				Mock<IModule> iModule = new Mock<IModule>();
				iModule.Setup(m => m.SystemName).Returns("SystemName");
				ModuleKeeper.Instance.Add(iModule.Object);
			}

			//first verify if instance are present, otherwise you could get a false positive test.
			Assert.IsTrue(ModuleKeeper.Instance.Modules.Count() > 0, "Could not test correctly since there is nothing to dispose");

			//the actual test
			ModuleKeeper.Instance.Clear();
			Assert.IsTrue(ModuleKeeper.Instance.Modules.ToArray().Count() == 0);
		}

		[TestMethod]
		public void GetModule_WithSystemName_ReturnsModule()
		{
			Mock<IModule> iModule = new Mock<IModule>();
			iModule.Setup(m => m.SystemName).Returns("SystemName");
			ModuleKeeper.Instance.Add(iModule.Object);

			Assert.AreEqual(ModuleKeeper.Instance.GetModule("SystemName"), iModule.Object);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetModule_WithNull_ThrowsArgumentNullException()
		{
			ModuleKeeper.Instance.GetModule(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ModuleNotFoundException))]
		public void GetModule_WithEmptyString_ThrowsModuleNotFoundException()
		{
			ModuleKeeper.Instance.GetModule(String.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(ModuleNotFoundException))]
		public void GetModule_WithUnknownSystemName_ThrowsModuleNotFoundException()
		{
			ModuleKeeper.Instance.GetModule("NotToBeFoundModule");
		}
	}
}
