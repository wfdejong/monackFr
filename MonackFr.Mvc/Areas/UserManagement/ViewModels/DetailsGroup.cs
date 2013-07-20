using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Mvc.Areas.UserManagement.Models;

namespace MonackFr.Mvc.Areas.UserManagement.ViewModels
{
	public class DetailsGroup
	{
		public Int32 Id { get; set; }

		public String Name { get; set; }

		public String Description { get; set; }

		public DateTime CreationDate { get; set; }

		public DateTime LastUpdate { get; set; }

		public List<CheckBoxListRole> GroupRoles { get; set; }

		public List<User> Users { get; set; }

		public List<CheckBoxListUser> GroupUsers { get; set; }

		public void Map(Group group)
		{
			group.Name = this.Name;
			group.Description = this.Description;
		}

		public void Fill(Group group)
		{
			this.Id = group.Id;
			this.Name = group.Name;
			this.Description = group.Description;
			this.CreationDate = group.CreationDate;
			this.LastUpdate = group.LastUpdate;
		}
	}
}