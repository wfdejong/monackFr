using System;

namespace MonackFr.Security
{
	public interface IAuthentication
	{
		/// <summary>
		/// Sets authentication cookie
		/// </summary>
		/// <param name="username"></param>
		/// <param name="createPersistentCookie"></param>
		void SetAuthCookie(string username, Boolean createPersistentCookie);

		/// <summary>
		/// Sign out current user
		/// </summary>
		void SignOut();
	}
}