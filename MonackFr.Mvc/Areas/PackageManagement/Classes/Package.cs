using MonackFr.Repository;
using MonackFr.Security;
using System;
using System.Collections.Generic;

namespace MonackFr.Mvc.Areas.PackageManagement
{
	/// <summary>
    /// Model of package
    /// </summary>
	public class Package
	{
		/// <summary>
		/// Collection of modules
		/// </summary>
		public IEnumerable<Module> Modules { get; set; }

		/// <summary>
		/// Collection of contexts
		/// </summary>
		public IEnumerable<IContext> Contexts { get; set; }

		/// <summary>
		/// relative path to the file
		/// </summary>
		public string RelativePath { get; set; }

		/// <summary>
		/// Pacakage name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Package description
		/// </summary>
		public string Description { get; set; }
	}
}