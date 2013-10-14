using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr.Security
{
	public interface IHash
	{
		string Create(string password);
		bool ValidatePassword(string password, string hash);
	}
}
