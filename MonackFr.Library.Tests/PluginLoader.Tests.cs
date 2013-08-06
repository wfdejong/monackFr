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
		public void TestAdd()
		{
			PluginLoader.Instance.Add(new Object());
			Assert.IsTrue(PluginLoader.Instance.Plugins.ToArray().Count() > 0, "PluginLoader.Add(), Plugin is not added");
			PluginLoader.Instance.Dispose();
		}

		[TestMethod]
		public void TestAddRange()
		{
			List<Object> range = new List<object>();
			range.Add(new Object());
			range.Add(new Object());
			PluginLoader.Instance.AddRange(range);
			Assert.IsTrue(PluginLoader.Instance.Plugins.ToArray().Count() == 2, "PluginLoader.AddRange, Add range is not working correctly");
		}

		[TestMethod]
		public void TestDispose()
		{
			PluginLoader.Instance.Add(new Object());
			Assert.IsTrue(PluginLoader.Instance.Plugins.ToArray().Count() > 0, "PluginLoader.Add(), Plugin is not added");
			PluginLoader.Instance.Dispose();
			Assert.IsTrue(PluginLoader.Instance.Plugins.ToArray().Count() == 0, "PluginLoader.Dispose(), Dispose is not working correctly");
		}
	}
}
