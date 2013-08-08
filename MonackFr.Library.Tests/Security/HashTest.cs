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
		public void create_a_hashed_password()
		{
			string password = "this is fun";

			// Generate a random salt
			RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
			byte[] salt = new byte[24];
			csprng.GetBytes(salt);

			// Hash the password and encode the parameters
			Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
			pbkdf2.IterationCount = 1000;
			byte[] hash = pbkdf2.GetBytes(24);
			
			string compareHash = 1000 + ":" +
				Convert.ToBase64String(salt) + ":" +
				Convert.ToBase64String(hash);
			string testHash = Hash.Create(password);

			Assert.AreEqual(compareHash, testHash);

		}
	}
}
