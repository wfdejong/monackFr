using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MonackFr.Library.Tests
{
	[TestClass]
	public class FormatTests
	{
		[TestMethod]
		public void ToCss_WithNull_ReturnsNull()
		{
			Assert.IsNull(Format.ToCss(null));
		}

		[TestMethod]
		public void ToCss_WithPoint_ReplaceToDash()
		{
			Assert.AreEqual("-", Format.ToCss("."));
		}
	}
}
