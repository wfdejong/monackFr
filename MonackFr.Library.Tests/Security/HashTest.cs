using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using MonackFr.Security;

namespace MonackFr.Library.Tests.Security
{
	[TestClass]
	public class HashTest
	{
		[TestMethod]
		public void create_a_hashed_password_and_validate_it()
		{
			string password = "admin";
            string testHash = Hash.Create(password);

            Assert.IsTrue(Hash.ValidatePassword(password, testHash));
        }
	}
}
