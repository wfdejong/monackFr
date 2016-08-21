using MonackFr.Library.Repository;
using System.ComponentModel.Composition;
using System.Data.Entity;

namespace MonackFr.Mvc.Contexts
{
	[Export(typeof(IContext))]
	public class PackageContext : Context, IContext
	{
        public DbSet<Entities.Package> Packages { get; set; }
        public DbSet<Entities.Module> Modules { get; set; }

		public PackageContext()
		{
			Database.SetInitializer<PackageContext>(null);
		}

		void IContext.Setup(DbModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Entities.Package>().ToTable("Packages");
            modelBuilder.Entity<Entities.Module>().ToTable("Modules");	
		}
	}
}