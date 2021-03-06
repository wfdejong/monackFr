﻿using System;
using System.Web.Security;

namespace MonackFr.Security
{
	public class Authentication : IAuthentication
	{
		/// <summary>
		/// Sets authentication cookie
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="createPersistentCookie"></param>
		public void SetAuthCookie(String userName, Boolean createPersistentCookie)
		{
			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
		}

		/// <summary>
		/// Sings out current user
		/// </summary>
		public void SignOut()
		{
			FormsAuthentication.SignOut();
		}
	}
}