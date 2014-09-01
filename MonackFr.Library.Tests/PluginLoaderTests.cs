using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using MonackFr.Module;
using Moq;

namespace MonackFr.Library.Tests
{
	[TestClass]
	public class ModuleKeeperTests
	{
		[TestMethod]
		public void Add_AddingOneObject_IncreasesNumberOfPluginsWithOne()
		{
			int pluginCount = ModuleKeeper.Instance.Modules.Count();
			Mock<IModule> iModule = new Mock<IModule>();
			ModuleKeeper.Instance.Add(iModule.Object);
			Assert.IsTrue(ModuleKeeper.Instance.Modules.Count() == pluginCount + 1);			
		}

		[TestMethod]
		public void Add_AddMultipleObjects_IncreasesNumberOfPlugins()
		{
			Mock<IModule> iModule1 = new Mock<IModule>();
			Mock<IModule> iModule2 = new Mock<IModule>();

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
			Mock<IModule> iModule = new Mock<IModule>();
			ModuleKeeper.Instance.Add(iModule.Object);			
			
			//first verify if instance are present, otherwise you could get a false positive test.
			Assert.IsTrue(ModuleKeeper.Instance.Modules.Count() > 0, "Could not test correctly since there is nothing to dispose");

			//the actual test
			ModuleKeeper.Instance.Clear();
			Assert.IsTrue(ModuleKeeper.Instance.Modules.ToArray().Count() == 0);
		}
	}
}
