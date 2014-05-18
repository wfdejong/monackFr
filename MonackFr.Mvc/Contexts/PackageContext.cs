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
        public DbSet<Package> Packages { get; set; }

		public PackageContext()
		{
			Database.SetInitializer<PackageContext>(null);
		}

		void IContext.Setup(DbModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Entities.Package>().ToTable("Packages");
            modelBuilder.Entity<Entities.Module>().ToTable("Modules");
            modelBuilder.Entity<Entities.Tile>().ToTable("Tiles");
            modelBuilder.Entity<Entities.MenuItem>().ToTable("MenuItems");	
		}
	}
}