using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonackFr.Module
{
	/// <summary>
	/// Definition of a module
	/// </summary>
	public interface IModule
	{
        /// <summary>
        /// The module name
        /// </summary>
        string Name { get; }

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
		MenuItem GetMenu();

		/// <summary>
		/// Returns module's tile
		/// </summary>
		/// <returns></returns>
		Tile GetTile(UrlHelper url);
	}
}