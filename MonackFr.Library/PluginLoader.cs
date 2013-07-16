using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr
{
	/// <summary>
	/// Singleton plugin loader
	/// </summary>
	public sealed class PluginLoader
	{
		/// <summary>
		/// static this for creating singleton
		/// </summary>
		private static readonly PluginLoader _pluginLoader = new PluginLoader();

		/// <summary>
		/// Plugin list
		/// </summary>
		private static List<object> _plugins = new List<object>();

		/// <summary>
		/// Plugin list
		/// </summary>
		public IEnumerable<object> Plugins
		{
			get
			{
				return _plugins;
			}
		}

		/// <summary>
		/// return this class
		/// </summary>
		public static PluginLoader Instance
		{
			get
			{				
				return _pluginLoader;
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public PluginLoader()
		{
		}

		/// <summary>
		/// Add a pluging to plugin list
		/// </summary>
		/// <param name="plugin"></param>
		public void Add(object plugin)
		{
			_plugins.Add(plugin);
		}

		/// <summary>
		/// Add plugins to plugin list
		/// </summary>
		/// <param name="plugins"></param>
		public void AddRange(IEnumerable<object> plugins)
		{
			_plugins.AddRange(plugins);
		}

		/// <summary>
		/// Dispose this object
		/// </summary>
		public void Dispose()
		{
			_plugins = new List<object>();
		}
	}
}
