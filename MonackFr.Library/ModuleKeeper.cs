using MonackFr.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonackFr
{
	/// <summary>
	/// Singleton with loaded Modules. Keeps module in memory for future use.
	/// TODO: change this into a module collection class, since only modules are put into memory for this.
	/// </summary>
	public sealed class ModuleKeeper
	{
		/// <summary>
		/// static this for creating singleton
		/// </summary>
		private static readonly ModuleKeeper _moduleKeeper = new ModuleKeeper();

		/// <summary>
		/// List with loaded objects
		/// </summary>
		private static Dictionary<string, IModule> _modules = new Dictionary<string, IModule>();

		/// <summary>
		/// List with loaded objects, converts internal Dictionary to IEnumerable.
		/// </summary>
		public IEnumerable<IModule> Modules
		{
			get
			{
				List<IModule> modules = new List<IModule>();

				foreach (KeyValuePair<string, IModule> module in _modules)
				{
					modules.Add(module.Value);
				}

				return modules;
			}
		}

		/// <summary>
		/// Creates and returns instance of ObjectLoader 
		/// </summary>
		public static ModuleKeeper Instance
		{
			get
			{
				return _moduleKeeper;
			}
		}

		/// <summary>
		/// Adds an object to objects list
		/// </summary>
		/// <param name="loadedModule"></param>
		public void Add(IModule loadedModule)
		{

			_modules.Add(loadedModule.SystemName, loadedModule);
		}

		/// <summary>
		/// Adds objects to objects list
		/// </summary>
		/// <param name="loadedModules"></param>
		public void AddRange(IEnumerable<IModule> loadedModules)
		{
			foreach (IModule module in loadedModules)
			{
				Add(module);
			}
		}

		/// <summary>
		/// Clears objects list.
		/// </summary>
		public void Clear()
		{
			_modules.Clear();
		}

		public IModule GetModule(string systemName)
		{
			if (!_modules.Keys.Contains(systemName))
			{
				throw new ModuleNotFoundException(string.Format("Module with name {0} could not be found", systemName));
			}

			return _modules[systemName];
		}
	}
}
