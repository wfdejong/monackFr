using MonackFr.Module;
using System.Collections.Generic;

namespace MonackFr.Mvc.Areas.PackageManagement
{
    public interface IDatabaseManager
    {
        void InstallDatabase(IEnumerable<IContext> contexts);
    }
}