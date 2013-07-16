using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MonackFr.Mvc.Areas.TaskList.Models
{
	public class Task
	{
		[Key]
		public Int32 Id { get; set; }

		private DateTime _InsertDate = DateTime.Now;
		public DateTime InsertDate { get; set; }

		private DateTime _LastUpdate = DateTime.Now;
		public DateTime LastUpdate { get; set; }
	}
}