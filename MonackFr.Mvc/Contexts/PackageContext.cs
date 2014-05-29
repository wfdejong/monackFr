using MonackFr.Module;
using MonackFr.Mvc.Entities;
using MonackFr.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Web;

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