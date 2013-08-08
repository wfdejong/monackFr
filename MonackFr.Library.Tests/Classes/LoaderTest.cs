using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonackFr.Library.Tests.Classes
{
	[Export(typeof(ILoaderTest))]
	class LoaderTest : ILoaderTest
	{
		public void LoaderTestMethod()
		{
		}
	}
}
