using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr.Wrappers
{
	public interface IRNGCryptoServiceProvider
	{
		void GetBytes(byte[] bytes);
	}
}
