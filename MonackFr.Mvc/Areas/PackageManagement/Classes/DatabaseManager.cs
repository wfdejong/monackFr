using MonackFr.Module;
using System.Collections.Generic;
using System.Data.Entity;

namespace MonackFr.Mvc.Areas.PackageManagement
{
	/// <summary>
	/// Manages database
	/// </summary>
    public class DatabaseManager : IDatabaseManager
    {
		/// <summary>
		/// Recreates the database
		/// </summary>
		/// <param name="contexts"></param>
        void IDatabaseManager.InstallDatabase(IEnumerable<IContext> contexts)
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