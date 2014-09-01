using System;
using System.Collections.Generic;

namespace MonackFr.Mvc.Areas.PackageManagement.ViewModels
{
	public class Package
	{
        public IEnumerable<Module> Modules { get; set; }
		
        public bool Installed { get; set; }

        public string RelativePath { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
	}
}