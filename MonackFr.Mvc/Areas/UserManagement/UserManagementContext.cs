using System.Data.Entity;
using MonackFr.Mvc.Areas.UserManagement.Models;
using MonackFr.Repository;
using System.Web.Security;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace MonackFr.Mvc.Areas.UserManagement
{
	[Export(typeof(IContext))]
	public class UserManagementContext : Context, IContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Group> Groups { get; set; }
		
		public UserManagementContext() {
			Database.SetInitializer<UserManagementContext>(null);
		}

		void IContext.Setup(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().ToTable("Users");
			modelBuilder.Entity<Role>().ToTable("Roles");
			modelBuilder.Entity<Group>().ToTable("Groups");
		}
	}
}