using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr
{
	/// <summary>
	/// Contains application information
	/// </summary>
	public static class Application
	{
		/// <summary>
		/// Application name
		/// </summary>
		private const string name = "MonackFr";

		/// <summary>
		/// Application descritption
		/// </summary>
		private const string description = "The Monack Framework";

		/// <summary>
		/// Package directory, relative to application root
		/// </summary>
		private static string packageDir = null;

		/// <summary>
		/// Application name
		/// </summary>
		public static string Name 
		{
			get { return name; }
		}

		/// <summary>
		/// Application description
		/// </summary>
		public static string Description
		{
			get { return description; }
		}

		/// <summary>
		/// Package directory, relative to application root
		/// </summary>
		public static string PackageDir
		{
			get
			{
				if (string.IsNullOrEmpty(packageDir)) 
				{
					packageDir = System.Web.Configuration.WebConfigurationManager.AppSettings["packagedir"].ToString().Trim('/', '\\');
				}
				return packageDir;
			}
		}
	}
}
