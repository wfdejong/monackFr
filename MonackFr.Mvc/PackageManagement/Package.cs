using System.Collections.Generic;
using MonackFr.Library.Repository;

namespace MonackFr.Mvc.PackageManagement
{
    public class Package
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// url to scripts
        /// </summary>
        public string ScriptSource { get; set; }

        /// <summary>
        /// Location of the .dll file of the package
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Context in the package
        /// </summary>
        public IEnumerable<IContext> Contexts { get; set; }

        /// <summary>
        /// Modules in the package
        /// </summary>
        public IEnumerable<Module> Modules { get; set; }
    }
}