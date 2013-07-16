using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MonackFr.Mvc.Areas.UserManagement.Models
{
	public class Group
	{
		public Int32 Id { get; set; }

		[Required]
		public String Name { get; set; }

		public String Description { get; set; }
		
		[Required]
		public DateTime CreationDate { get; set; }

		[Required]
		public DateTime LastUpdate { get; set; }

		[InverseProperty("Groups")]
		public virtual ICollection<Role> Roles { get; set; }

		[InverseProperty("Groups")]
		public virtual ICollection<User> Users { get; set; }

		public Group()
		{
			CreationDate = DateTime.Now;
			LastUpdate = DateTime.Now;
		}
	}
}