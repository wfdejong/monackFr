using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonackFr.Repository
{
	/// <summary>
	/// Definition of a module
	/// </summary>
	public interface IModule
	{
		/// <summary>
		/// The name of the module
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The unique name within the system
		/// </summary>
		string SystemName { get; }

		/// <summary>
		/// Description of the module
		/// </summary>
		string Description { get; }

		/// <summary>
		/// The author of the module
		/// </summary>
		string Author { get; }

		/// <summary>
		/// Returns menu items
		/// </summary>
		/// <returns></returns>
		IEnumerable<MenuItem> GetMenu(UrlHelper urlHelper);

		/// <summary>
		/// Returns module's tile
		/// </summary>
		/// <returns></returns>
		Tile GetTile(UrlHelper urlHelper);
	}
}