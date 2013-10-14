using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace MonackFr.Library.Tests
{
	[TestClass]
	public class PluginLoaderTests
	{
		[TestMethod]
		public void Add_AddingOneObject_IncreasesNumberOfPluginsWithOne()
		{
			int pluginCount = PluginLoader.Instance.Plugins.Count();
			PluginLoader.Instance.Add(new Object());
			Assert.IsTrue(PluginLoader.Instance.Plugins.Count() == pluginCount + 1);			
		}

		[TestMethod]
		public void Add_AddMultipleObjects_IncreasesNumberOfPlugins()
		{
			List<Object> range = new List<object>()
				{
					new object(),
					new object()
				};
			int pluginCount = PluginLoader.Instance.Plugins.Count();
			PluginLoader.Instance.AddRange(range);
			Assert.IsTrue(PluginLoader.Instance.Plugins.Count() == pluginCount + 2);
		}

		[TestMethod]
		public void Dispose_DisposePluginLoader_RemovesAllPlugins()
		{
			PluginLoader.Instance.Add(new Object());			
			
			//first verify if instance are present, otherwise you could get a false positive test.
			Assert.IsTrue(PluginLoader.Instance.Plugins.Count() > 0, "Could not test correctly since there is nothing to dispose");

			//the actual test
			PluginLoader.Instance.Dispose();
			Assert.IsTrue(PluginLoader.Instance.Plugins.ToArray().Count() == 0);
		}
	}
}
