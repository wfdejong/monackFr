using System;
using System.Web.Configuration;

namespace MonackFr
{
	/// <summary>
	/// Contains application information
	/// </summary>
	public static class ApplicationSettings
	{
		/// <summary>
		/// Application name
		/// </summary>
		private const string _name = "MonackFr";

		/// <summary>
		/// Application descritption
		/// </summary>
		private const string _description = "The Monack Framework";

		/// <summary>
		/// Package directory, relative to application root
		/// </summary>
		private static string _packageDir = null;

		/// <summary>
		/// true if db is installed.
		/// </summary>
		private static bool? _dbInstalled = null;

		/// <summary>
		/// Application name
		/// </summary>
		public static string Name 
		{
			get { return _name; }
		}

		/// <summary>
		/// Application description
		/// </summary>
		public static string Description
		{
			get { return _description; }
		}

		/// <summary>
		/// Package directory, relative to application root
		/// </summary>
		public static string PackageDir
		{
			get
			{
				if (string.IsNullOrEmpty(_packageDir)) 
				{
					_packageDir = System.Web.Configuration.WebConfigurationManager.AppSettings["packagedir"].ToString().Trim('/', '\\');
				}
				return _packageDir;
			}
		}

		public static bool DbInstalled
		{
			get
			{
				if (_dbInstalled.HasValue)
				{
					return _dbInstalled.Value;
				}
				else
				{
					bool dbInstalled;
					string installed = WebConfigurationManager.AppSettings["DbInstalled"];
					if (Boolean.TryParse(installed, out dbInstalled))
					{
						_dbInstalled = dbInstalled;
						return _dbInstalled.Value;
					}
				}

				return false;
			}
		}
	}
}
