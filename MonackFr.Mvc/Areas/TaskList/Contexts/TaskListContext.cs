using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.Composition;
using MonackFr.Module;
using MonackFr.Mvc.Areas.TaskList.Entities;

namespace MonackFr.Mvc.Areas.TaskList.Contexts
{
	[Export(typeof(IContext))]
	public class TaskListContext : Context, IContext
	{
		public DbSet<Task> Tasks { get; set; }

		public TaskListContext() {
			Database.SetInitializer<TaskListContext>(null);
		}

		void IContext.Setup(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Task>().ToTable("Tasks");
			modelBuilder.Entity<Task>().HasKey(task => task.Id);			
		}
	}
}