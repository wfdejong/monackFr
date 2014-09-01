using MonackFr.Module;
using System.Collections.Generic;

namespace MonackFr.Mvc.Areas.PackageManagement
{
	/// <summary>
	/// Manages database
	/// </summary>
    public interface IDatabaseManager
    {
		/// <summary>
		/// Recreates the database
		/// </summary>
		/// <param name="contexts"></param>
        void InstallDatabase(IEnumerable<IContext> contexts);
    }
}