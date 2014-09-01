using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.PackageManagement.Entities
{
	/// <summary>
	/// Thrown if loaded package is null
	/// </summary>
	public class PackageNullReferenceException : NullReferenceException
	{
		public PackageNullReferenceException() : base() { }

		public PackageNullReferenceException(string message) : base(message) { }
	}
}