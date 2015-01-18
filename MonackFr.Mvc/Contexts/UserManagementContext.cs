using System.Data.Entity;
using MonackFr.Repository;
using System.Web.Security;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using MonackFr.Repository;
using MonackFr.Mvc.Entities;

namespace MonackFr.Mvc.Contexts
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
			modelBuilder.Entity<User>()
				.HasMany(u => u.Roles)
				.WithMany(r => r.Users)
				.Map(u =>
				{
					u.MapLeftKey("User_Id");
					u.MapRightKey("Role_Id");
					u.ToTable("UserRoles");
				});

			modelBuilder.Entity<User>()
				.HasMany(u => u.Groups)
				.WithMany(g => g.Users)
				.Map(u =>
				{
					u.MapLeftKey("User_Id");
					u.MapRightKey("Group_Id");
					u.ToTable("UserGroups");
				});
			
			modelBuilder.Entity<Role>()
				.HasMany(r => r.Groups)
				.WithMany(g =>g.Roles)
				.Map(m => 
				{
					m.MapLeftKey("Role_Id");
					m.MapRightKey("Group_Id");
					m.ToTable("RoleGroups");
				});			
		}

	}
}