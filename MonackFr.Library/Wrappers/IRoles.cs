using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace MonackFr.Wrappers
{
	public interface IRoles
	{
		void CreateRole(IMfrRole role);
	}
}
