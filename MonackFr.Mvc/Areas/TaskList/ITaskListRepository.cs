using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Repository;
using MonackFr.Mvc.Areas.TaskList.Models;

namespace MonackFr.Mvc.Areas.TaskList
{
	public interface ITaskListRepository : IGenericRepository<Task>
	{
	}
}