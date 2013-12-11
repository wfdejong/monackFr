using MonackFr.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc
{
    public interface IPackage
    {
        void LoadContexts();
        
        void LoadModules();
        
        IEnumerable<IContext> Contexts
        { get; }

        IEnumerable<IModule> Modules
        { get; }
    }
}