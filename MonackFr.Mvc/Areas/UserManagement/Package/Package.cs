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
        string IPackage.Name { get { return "UserManagement"; } }
        string IPackage.Description { get { return "User management module, with provider"; } }
    }
}