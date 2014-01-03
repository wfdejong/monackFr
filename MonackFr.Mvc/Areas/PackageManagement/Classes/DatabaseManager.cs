using MonackFr.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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