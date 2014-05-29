using MonackFr.Module;
using MonackFr.Repository;
using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Module
{
    public interface IPackage
    {
        /// <summary>
        /// Package name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Package description
        /// </summary>
        string Description { get; }        
    }
}