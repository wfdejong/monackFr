using MonackFr.Module;
using System.Data.Entity;

namespace MonackFr.Mvc.Areas.PackageManagement
{
    public class DatabaseManager : IDatabaseManager
    {
        void IDatabaseManager.InstallDatabase(IContext[] contexts)
        {
            using (Context context = new Context())
            {
                context.Contexts = contexts;
                Database.SetInitializer(new DropCreateDatabaseAlways<Context>());
                context.Database.Initialize(true);
            }
        }
    }
}