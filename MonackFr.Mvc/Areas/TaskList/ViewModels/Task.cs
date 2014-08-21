using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonackFr.Mvc.Areas.TaskList.ViewModels
{
	public class Task
	{
		public Int32 Id { get; set; }

        public DateTime Created { get; set; }

		public DateTime LastUpdate { get; set; }

		[Required]
		public DateTime Ends { get; set; }

		public int CreatedBy { get; set; }

		[Required]
		public int AssignedTo { get; set; }
	}
}