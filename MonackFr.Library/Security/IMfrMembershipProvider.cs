using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr.Security
{
	public interface IMfrMembershipProvider
	{
		/// <summary>
		/// returns monack framework  user
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		MfrUser GetMfrUser(string userName);
	}
}
