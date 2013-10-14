using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr.Wrappers
{
	public interface IRfc2898DeriveBytes
	{
		int IterationCount { set; }
		string Password { set; }
		byte[] Salt { get; set; }

		byte[] GetBytes(int length);
	}
}
