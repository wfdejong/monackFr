﻿namespace MonackFr.Wrappers
{
	public class RNGCryptoServiceProvider : IRNGCryptoServiceProvider
	{
		private System.Security.Cryptography.RNGCryptoServiceProvider provider = new System.Security.Cryptography.RNGCryptoServiceProvider();

		void IRNGCryptoServiceProvider.GetBytes(byte[] bytes)
		{
			provider.GetBytes(bytes);
		}
	}
}
