using System;
using System.Collections.Generic;

namespace MonackFr.Mvc.Entities
{
    /// <summary>
    /// Database model for package
    /// </summary>
    public class Package
    {
        /// <summary>
        /// The id of the package
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Name of package
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Relative path (seen from app root) to the file of the package
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Relative url to (java) scripts
        /// </summary>
        public string ScriptSource { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The modules in this package
        /// </summary>
        public ICollection<Module> Modules { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Package()
        {
            Created = DateTime.Now;
        }
    }
}