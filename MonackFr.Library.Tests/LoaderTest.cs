using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using MonackFr;
using System.Linq;

namespace MonackFr.Library.Tests
{
	[TestClass]
	public class LoaderTest
	{
		[TestMethod]
		[ExpectedException(typeof(System.IO.FileNotFoundException))]
		public void file_not_found_exeption()
		{
			string path = @"D:\code\MonackFr2\packages\Moq 4.0.10827\Moq.dll2"; //non existing file
			Loader<object> loader = new Loader<object>(path);
		}

		[TestMethod]
		public void compose_parts_is_hit()
		{
			var path = System.Reflection.Assembly.GetAssembly(typeof(LoaderTest)).Location;
			Loader<Classes.ILoaderTest> loader = new Loader<Classes.ILoaderTest>(path);
			Assert.AreEqual(loader.LoadedItems.Count(), 1);
		}

	}
}
