using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MonackFr.Mvc.Entities
{
	public class Group
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }
		
		[Required]
		public DateTime CreationDate { get; set; }

		[Required]
		public DateTime LastUpdate { get; set; }

		public virtual ICollection<Role> Roles { get; set; }

		public virtual ICollection<User> Users { get; set; }

		public Group()
		{
			CreationDate = DateTime.Now;
			LastUpdate = DateTime.Now;
		}
	}
}