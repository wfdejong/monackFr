using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MonackFr.Mvc.Areas.TaskList.Entities
{
	public class Task
	{
		public Int32 Id { get; set; }

        public DateTime InsertDate { get; set; }

		public DateTime LastUpdate { get; set; }

        public Task()
        {
            InsertDate = DateTime.Now;
            LastUpdate = DateTime.Now;
        }
	}
}