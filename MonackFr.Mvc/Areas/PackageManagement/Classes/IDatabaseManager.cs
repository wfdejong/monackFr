using MonackFr.Module;

namespace MonackFr.Mvc.Areas.PackageManagement
{
    public interface IDatabaseManager
    {
        void InstallDatabase(IContext[] contexts);
    }
}