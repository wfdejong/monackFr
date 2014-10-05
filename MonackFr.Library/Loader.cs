using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MonackFr
{
	/// <summary>
	/// Generic loader. Loads libraries of interface T of given file.
	/// </summary>
	/// <typeparam name="T">Interface to load</typeparam>
	public class Loader<T> : ILoader<T>
	{
		#region private fields

		/// <summary>
		/// List with plugins
		/// </summary>
		[ImportMany]
		private List<T> _loadedItems = null;

		#endregion //private fields

		#region test stubs

		private Wrappers.IFile _file = new Wrappers.File();
		private Wrappers.ICompositionContainer _compositionconatiner;

		#endregion

		#region constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public Loader()
		{
			_file = new Wrappers.File();
		}

        /// <summary>
        /// Constructor for unit tests
        /// </summary>
        /// <param name="path">path to file that containes interface T</param>
		public Loader(Wrappers.IFile file, Wrappers.ICompositionContainer container)
		{
			_compositionconatiner = container;
			_file = file;
		}

		#endregion //constructors

		#region ILoader

		/// <summary>
		/// Loaded plugins
		/// </summary>
		IEnumerable<T> ILoader<T>.LoadedItems
		{
			get
			{
				return _loadedItems;
			}
		}
				
		/// <summary>
		/// Loads the plugins from the set file
		/// </summary>		
		ILoader<T> ILoader<T>.Load(string path)
		{
			if (String.IsNullOrEmpty(path))
			{
				throw new NullReferenceException("Path is not set");
			}

			if (!_file.Exists(path))
			{
				throw new System.IO.FileNotFoundException();
			}

			_compositionconatiner = new Wrappers.CompositionContainer();
			//_compositionconatiner.Path = path;
			_compositionconatiner.ComposeParts(this);
			
			return this;
		}

		#endregion //ILoader
	}
}
