using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.PackageManagement
{
    public class Module
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public IEnumerable<IMfrRole> Roles { get; set; }
    }
}