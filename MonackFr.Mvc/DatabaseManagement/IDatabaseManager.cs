using System.Collections.Generic;
using MonackFr.Library.Repository;

namespace MonackFr.Mvc.DatabaseManagement
{
    internal interface IDatabaseManager
    {
        /// <summary>
        /// Recreates the database
        /// </summary>
        /// <param name="contexts"></param>
        void InstallDatabase(IEnumerable<IContext> contexts);
    }
}