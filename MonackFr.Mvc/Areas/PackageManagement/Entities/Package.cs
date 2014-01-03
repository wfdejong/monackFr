using MonackFr.Mvc.Module;
using MonackFr.Repository;
using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonackFr.Mvc.Areas.PackageManagement.Entities
{
	public class Package : IPackage
	{
		private IEnumerable<IModule> _modules;
		private IEnumerable<IContext> _contexts;
        private IEnumerable<IAuthorization> _authorizations;
		private readonly string baseDir = AppDomain.CurrentDomain.BaseDirectory;
		private string _path;

		[NotMapped]
		public IEnumerable<IModule> Modules
		{
			get
			{
				return _modules;
			}
		}
		[NotMapped]
		public bool Installed { get; set; }

		[NotMapped]
		public IEnumerable<IContext> Contexts
		{
			get
			{
				return _contexts;
			}
		}
		
        [NotMapped]
        public IEnumerable<IAuthorization> Authorizations
        {
            get
            {
                return _authorizations;
            }
        }

		[Key]
		public int Id
		{
			get;
			set;
		}

		[Required]
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