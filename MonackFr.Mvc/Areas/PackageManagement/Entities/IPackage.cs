using MonackFr.Mvc.Module;
using MonackFr.Repository;
using MonackFr.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.PackageManagement.Entities
{
    public interface IPackage
    {
        void LoadContexts();

        void LoadModules();

        void LoadAuthorizations();

        IEnumerable<IContext> Contexts { get; }

        IEnumerable<IModule> Modules { get; }

        IEnumerable<IAuthorization> Authorizations { get; }

        string Path { get; set; }
    }
}