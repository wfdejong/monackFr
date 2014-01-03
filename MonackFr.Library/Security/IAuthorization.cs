using System.Collections.Generic;

namespace MonackFr.Security
{	
	public interface IAuthorization
	{
		/// <summary>
		/// Returns roles
		/// </summary>
		/// <returns></returns>
		List<IMfrRole> GetRoles();		
	}
}