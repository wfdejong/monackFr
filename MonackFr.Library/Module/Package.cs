using MonackFr.Module;
using MonackFr.Security;
using System;
using System.Collections.Generic;

namespace MonackFr.Module
{
	public class Package : IPackage
	{
		private IEnumerable<IModule> _modules;
        private IEnumerable<MonackFr.Module.IContext> _contexts;
        private IEnumerable<IAuthorization> _authorizations;
		private readonly string baseDir = AppDomain.CurrentDomain.BaseDirectory;
		private string _path;

		public IEnumerable<IModule> Modules
		{
			get
			{
				return _modules;
			}
		}
		
        public bool Installed { get; set; }
        		
        public IEnumerable<IContext> Contexts
		{
			get
			{
				return _contexts;
			}
		}
		
        public IEnumerable<IAuthorization> Authorizations
        {
            get
            {
                return _authorizations;
            }
        }

		public string Path
		{
			get
			{
				return _path;
			}
			set
			{
				_path = value;
			}

		}

		public Package() { }

		public Package(string path)
		{
			_path = path;
		}

		public void LoadModules()
		{
			_modules = new Loader<IModule>(string.Format(@"{0}{1}", baseDir, _path)).LoadedItems;
		}

		public void LoadContexts()
		{
			_contexts = new Loader<IContext>(string.Format(@"{0}{1}", baseDir, _path)).LoadedItems;
		}

        public void LoadAuthorizations()
        {
            _authorizations = new Loader<IAuthorization>(string.Format(@"{0}{1}", baseDir, _path)).LoadedItems;
        }
	}
}