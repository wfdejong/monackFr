using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonackFr.Library.Module
{
    /// <summary>
    /// Defines angular states
    /// </summary>
    public class State
    {
        /// <summary>
        /// state name (should be unique), can be nested (name-x/name-y)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// the configuration of the state like in angular
        /// </summary>
        public string Config { get; set; }
    }
}
