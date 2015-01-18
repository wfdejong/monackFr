using MonackFr.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.TaskList
{
    [Export(typeof(IPackage))]
    public class Package : IPackage
    {
        string IPackage.Name { get { return "TaskList"; } }
        string IPackage.Description { get { return "Managing task in this simple list"; } }
    }
}