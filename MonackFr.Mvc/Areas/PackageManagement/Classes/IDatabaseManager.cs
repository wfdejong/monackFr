using MonackFr.Module;
using MonackFr.Repository;
using System.Data.Entity;

namespace MonackFr.Mvc.Areas.PackageManagement
{
    public interface IDatabaseManager
    {
        void InstallDatabase(IContext[] contexts);
    }
}