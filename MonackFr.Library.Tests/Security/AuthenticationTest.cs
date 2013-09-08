using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonackFr.Security;
using System.Web.Security;
using System.Web;

namespace MonackFr.Library.Tests.Security
{
	[TestClass]
	public class AuthenticationTest
	{
		[TestMethod]
		public void set_authentication_cookie()
		{
			IAuthentication authentication = new Authentication();
			authentication.SetAuthCookie("username", true);
			HttpCookie cookie = FormsAuthentication.GetAuthCookie("username", true);
			Assert.IsNotNull(cookie);
		}
	}
}
