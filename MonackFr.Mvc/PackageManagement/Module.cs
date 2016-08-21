using System.Collections.Generic;
using MonackFr.Security;

namespace MonackFr.Mvc.PackageManagement
{
    public class Module
    {
        /// <summary>
		/// Module name
		/// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Module description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Module author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// System name of module. Should be unique within the whole application
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// roles available in module
        /// </summary>
        public IEnumerable<IMfrRole> Roles { get; set; }
    }
}