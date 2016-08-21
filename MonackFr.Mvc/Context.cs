using System.Collections.Generic;
using System.Data.Entity;
using MonackFr.Library.Repository;

namespace MonackFr
{
	/// <summary>
	/// Base context, contains list with subcontexts of all packages
	/// </summary>
	public class Context : DbContext
	{
		/// <summary>
		/// List with subcontexts
		/// </summary>
		public IEnumerable<IContext> Contexts { get; set; }

		public Context()
			: base("Context")
		{
			Database.SetInitializer<Context>(null);
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
                        
			if (Contexts != null)
			{
				foreach (IContext context in Contexts)
					context.Setup(modelBuilder);
			}
		}
	}
}