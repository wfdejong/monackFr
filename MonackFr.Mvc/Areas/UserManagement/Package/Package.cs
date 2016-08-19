using MonackFr.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.UserManagement.Package
{
    [Export(typeof(IPackage))]
    public class Package : IPackage
    {
        /// <summary>
        /// Name of the package
        /// </summary>
        string IPackage.Name { get { return "UserManagement"; } }

        /// <summary>
        /// Description of the package
        /// </summary>
        string IPackage.Description { get { return "User management module, with provider"; } }

        /// <summary>
        /// Script bundle of the package
        /// </summary>
        string IPackage.ScriptSource { get { return @"~/scripts/usermanagement"; } }
    }
}