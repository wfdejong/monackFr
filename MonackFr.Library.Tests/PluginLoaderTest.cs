using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonackFr.Library.Tests
{
	[TestClass()]
	public class PluginLoaderTest
	{
		[TestMethod()]
		public void PluginLoaderSingleton()
		{
			PluginLoader.Instance.Add(new TestClass{ testdata = "testdata" });

			TestClass test = (TestClass)PluginLoader.Instance.Plugins.ToArray()[0];
			Assert.IsTrue(test.testdata == "testdata");
		}
	}

	internal class TestClass
	{
		internal string testdata { get; set; }		
	}
}
