using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace MonackFr.Security
{
	public class Hash
	{
		// The following constants may be changed without breaking existing hashes.
		/// <summary>
		/// Length of salt in bytes.
		/// Can be changed without breaking the existing hashes
		/// </summary>
		private const int saltLength = 24;

		/// <summary>
		/// Length of hash in bytes
		/// </summary>
		private const int hashLength = 24;

		/// <summary>
		/// number of iterations
		/// </summary>
		private const int iterations = 1000;
		
		/// <summary>
		/// location of iterations in final string
		/// </summary>
		private const int iterationIndex = 0;

		/// <summary>
		/// location of saltindex in final string
		/// </summary>
		private const int saltIndex = 1;

		/// <summary>
		/// location of the hash in final string
		/// </summary>
		private const int hashIndex = 2;

		/// <summary>
		/// Creates a salted PBKDF2 hash of the password and combines this in a 
		/// colon seperated string with iterations and the salt
		/// </summary>
		/// <param name="password">The password to hash.</param>
		/// <returns>The hash, iterations and salt of the password in format "[iterations]:[salt]:[hash]</returns>
		public static string Create(string password)
		{
			// Generate a random salt
			RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
			byte[] salt = new byte[saltLength];
			csprng.GetBytes(salt);

			// Hash the password and encode the parameters
			byte[] hash = PBKDF2(password, salt, iterations, hashLength);
			return iterations + ":" +
				Convert.ToBase64String(salt) + ":" +
				Convert.ToBase64String(hash);
		}

		/// <summary>
		/// Validates a password given a hash, iterations and salt.
		/// </summary>
		/// <param name="password">The password to check.</param>
		/// <param name="hashCombo">The hash, iterations and salt of the password in format "[iterations]:[salt]:[hash]</param>
		/// <returns>True if the password is correct. False otherwise.</returns>
		public static bool ValidatePassword(string password, string hashCombo)
		{
			// Extract the parameters from the hash
			char[] delimiter = { ':' };
			string[] split = hashCombo.Split(delimiter);
			int iterations = Int32.Parse(split[iterationIndex]);
			byte[] salt = Convert.FromBase64String(split[saltIndex]);
			byte[] hash = Convert.FromBase64String(split[hashIndex]);

			byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
			return SlowEquals(hash, testHash);
		}

		/// <summary>
		/// Compares two byte arrays in length-constant time.
		/// </summary>
		/// <param name="a">Byte array.</param>
		/// <param name="b">Byte array.</param>
		/// <returns>True if both byte arrays are equal. False otherwise.</returns>
		private static bool SlowEquals(byte[] a, byte[] b)
		{
			uint diff = (uint)a.Length ^ (uint)b.Length;
			for (int i = 0; i < a.Length && i < b.Length; i++)
				diff |= (uint)(a[i] ^ b[i]);
			return diff == 0;
		}

		/// <summary>
		/// Computes the PBKDF2-SHA1 hash of a password.
		/// </summary>
		/// <param name="password">The password to hash.</param>
		/// <param name="salt">The salt.</param>
		/// <param name="iterations">The iteration count.</param>
		/// <param name="length">The length of the hash to generate, in bytes.</param>
		/// <returns>A hash of the password.</returns>
		private static byte[] PBKDF2(string password, byte[] salt, int iterations, int length)
		{
			Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
			pbkdf2.IterationCount = iterations;
			return pbkdf2.GetBytes(length);
		}
	}

}

