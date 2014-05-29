using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonackFr.Module
{
	/// <summary>
	/// Interface to implement a module
	/// </summary>
	public interface IModule
	{
        string Name { get; }

        string Description { get; }

        string Author { get; }
        
        /// <summary>
        /// Returns meta data of module
        /// </summary>
        Dictionary<string, string> MetaData { get; }
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