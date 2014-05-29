using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Repository;
using MonackFr.Mvc.Areas.TaskList.Entities;

namespace MonackFr.Mvc.Areas.TaskList.Repositories
{
	public interface ITaskListRepository : IGenericRepository<Task>
	{
	}
}