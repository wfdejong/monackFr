using MonackFr.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.PackageManagement.Contexts
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
			modelBuilder.Entity<Package>()
				.ToTable("Packages");			
		}
	}
}