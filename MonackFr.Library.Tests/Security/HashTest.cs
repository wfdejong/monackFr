using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using MonackFr.Security;
using MonackFr.Wrappers;
using Moq;

namespace MonackFr.Library.Tests.SecurityTests
{
	[TestClass]
	public class HashTest
	{
		Mock<IRNGCryptoServiceProvider> _cryptoProvider;
		Mock<IRfc2898DeriveBytes> _deriveBytes;

		[TestInitialize]
		public void Initialize()
		{
			_cryptoProvider = new Mock<IRNGCryptoServiceProvider>();
			_deriveBytes = new Mock<IRfc2898DeriveBytes>();
		}

		[TestCleanup]
		public void CleanUp()
		{
			_cryptoProvider = null;
			_deriveBytes = null;
		}

		#region Create
		
		[TestMethod]
		public void Create_WithPassword_RunsGetBytesFromCryptoProvider()
		{		
			IHash hash = new Hash(_cryptoProvider.Object, _deriveBytes.Object);
			hash.Create("password");
			
			_cryptoProvider.Verify(c => c.GetBytes(It.IsAny<byte[]>()), Times.Exactly(1));            
        }

		[TestMethod]
		public void Create_WithPassword_Sets_Salt_For_PBKDF2()
		{			
			IHash hash = new Hash(_cryptoProvider.Object, _deriveBytes.Object);
			hash.Create("password");

			_deriveBytes.VerifySet(c => c.Salt=It.IsAny<byte[]>());
		}

		[TestMethod]
		public void Create_WithPassword_Sets_Password_For_PBKDF2()
		{			
			IHash hash = new Hash(_cryptoProvider.Object, _deriveBytes.Object);
			hash.Create("password");

			_deriveBytes.VerifySet(c => c.Password = It.IsAny<string>());
		}

		[TestMethod]
		public void Create_WithPassword_Sets_IterationCount_For_PBKDF2()
		{
			IHash hash = new Hash(_cryptoProvider.Object, _deriveBytes.Object);
			hash.Create("password");

			_deriveBytes.VerifySet(c => c.IterationCount = It.IsAny<int>());
		}

		#endregion //Create
		
		#region ValidatePassword
				
		[TestMethod]
		public void ValidatePassword_WithPassword_Sets_Salt_For_PBKDF2()
		{
			IHash hash = new Hash(_cryptoProvider.Object, _deriveBytes.Object);
			hash.ValidatePassword("password", "10:salt:hash");

			_deriveBytes.VerifySet(c => c.Salt = It.IsAny<byte[]>());
		}

		[TestMethod]
		public void ValidatePassword_WithPassword_Sets_Password_For_PBKDF2()
		{
			IHash hash = new Hash(_cryptoProvider.Object, _deriveBytes.Object);
			hash.ValidatePassword("password", "10:salt:hash");

			_deriveBytes.VerifySet(c => c.Password = It.IsAny<string>());
		}

		[TestMethod]
		public void ValidatePassword_WithPassword_Sets_IterationCount_For_PBKDF2()
		{
			IHash hash = new Hash(_cryptoProvider.Object, _deriveBytes.Object);
			hash.ValidatePassword("password", "10:salt:hash");

			_deriveBytes.VerifySet(c => c.IterationCount = It.IsAny<int>());
		}

		#endregion //ValidatePassword
	}
}
