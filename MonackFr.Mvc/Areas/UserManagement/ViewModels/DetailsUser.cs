using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MonackFr.Mvc.Entities;

namespace MonackFr.Mvc.Areas.UserManagement.ViewModels
{
	public class DetailsUser
	{
		public Int32 Id { get; set; }

		public String UserName { get; set; }

		public String Email { get; set; }

		public DateTime CreationDate { get; set; }

		public DateTime LastUpdate { get; set; }

		public DateTime? LastLogin { get; set; }
				
		public List<CheckBoxListRole> UserRoles { get; set; }

		public List<CheckBoxListGroup> UserGroups { get; set; }

		public DetailsUser()
		{
		}
				
		public void Map(User user)
		{
			user.UserName = this.UserName;
			user.Email = this.Email;						
		}

		public void Fill(User user)
		{
			Id = user.Id;
			UserName = user.UserName;
			Email = user.Email;
			CreationDate = user.Creation;
			LastUpdate = user.LastUpdate;
			LastLogin = user.LastLogin;	
		}
	}
}