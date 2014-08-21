using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MonackFr.Mvc.Areas.TaskList.Entities
{
	public class Task
	{
		public int Id { get; set; }

		[Required]
        public DateTime Created { get; set; }

		[Required]
		public DateTime LastUpdate { get; set; }

		[Required]
		public DateTime Ends { get; set; }

		[Required]
		public int CreatedBy { get; set; }

		[Required]
		public int AssignedTo { get; set; }

        public Task()
        {
            Created = DateTime.Now;
            LastUpdate = DateTime.Now;			
        }
	}
}