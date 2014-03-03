using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Module
{
	/// <summary>
	/// Interface to implement a module
	/// </summary>
	public interface IModule
	{
		/// <summary>
		/// Returns menu items
		/// </summary>
		/// <returns></returns>
		MenuItem GetMenu();

		/// <summary>
		/// Returns meta data of module
		/// </summary>
		Dictionary<string, string> MetaData { get; }

		/// <summary>
		/// Returns module's tile
		/// </summary>
		/// <returns></returns>
		Tile GetTile();
	}
}