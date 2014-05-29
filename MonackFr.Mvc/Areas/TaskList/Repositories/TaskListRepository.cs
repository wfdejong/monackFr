using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Repository;
using MonackFr.Mvc.Areas.TaskList.Entities;
using MonackFr.Mvc.Areas.TaskList.Contexts;

namespace MonackFr.Mvc.Areas.TaskList.Repositories
{
	public class TaskListRepository : GenericRepository<TaskListContext, Task>, ITaskListRepository
	{
	}
}