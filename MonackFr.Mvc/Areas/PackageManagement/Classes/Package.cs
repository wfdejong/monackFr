using MonackFr.Module;
using MonackFr.Security;
using System;
using System.Collections.Generic;

namespace MonackFr.Mvc.Areas.PackageManagement
{
	public class Package
	{
        public IEnumerable<Module> Modules { get; set; }
        public IEnumerable<IContext> Contexts { get; set; }       
        public string RelativePath { get; set; }        
        public string Name { get; set; }
        public string Description { get; set; }
	}
}