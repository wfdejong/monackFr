using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MonackFr
{
	/// <summary>
	/// Generic loader. Autmotically loads libraries of interface T of given file.
	/// </summary>
	/// <typeparam name="T">Interface to load</typeparam>
	public class Loader<T>
	{
		/// <summary>
		/// List with plugins
		/// </summary>
		[ImportMany]
		private List<T> _available = null;

		#region test stubs

		private Wrappers.IFile _file = new Wrappers.File();
		private Wrappers.ICompositionContainer _compositionconatiner;

		#endregion

		/// <summary>
		/// Loaded plugins
		/// </summary>
		public IEnumerable<T> LoadedItems
		{
			get
			{
				return _available;
			}
		}

		/// <summary>
		/// Loads all libraries of type T in file.
		/// </summary>
		/// <param name="path">path to file that containes interface T</param>
		public Loader(string path)
		{
			this.Load(path);
		}

        /// <summary>
        /// Loads all libraries of type T in file.
        /// </summary>
        /// <param name="path">path to file that containes interface T</param>
		public Loader(string path, Wrappers.IFile file, Wrappers.ICompositionContainer container)
		{
			_file = file;
			_compositionconatiner = container;
			this.Load(path);
		}
		
		/// <summary>
		/// Load the plugings from the file
		/// </summary>
		/// <param name="path"></param>
		private void Load(string path)
		{
			if (_file.Exists(path))
			{
				if (_compositionconatiner == null)
				{
					_compositionconatiner = new Wrappers.CompositionContainer(path);
				}

				_compositionconatiner.ComposeParts(this);
			}
			else
			{
				throw new System.IO.FileNotFoundException();
			}			
		}
	}
}
