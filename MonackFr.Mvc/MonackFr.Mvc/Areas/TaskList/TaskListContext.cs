using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Repository;
using MonackFr.Mvc.Areas.TaskList.Models;
using System.Data.Entity;
using System.ComponentModel.Composition;

namespace MonackFr.Mvc.Areas.TaskList
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