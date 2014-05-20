using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MonackFr.Security;

namespace MonackFr.Mvc.Entities
{
	public class Role : MfrRole
	{
		[InverseProperty("Roles")]
		public virtual ICollection<User> Users { get; set; }

		[InverseProperty("Roles")]
		public virtual ICollection<Group> Groups { get; set; }
		
		public Role() 
		{
			Creation = DateTime.Now;
			LastUpdate = DateTime.Now;
		}

		public Role(IMfrRole mfrRole)
		{
			if (mfrRole.Id != 0)
			{
				Id = mfrRole.Id;
			}
			Name = mfrRole.Name;
			Description = mfrRole.Description;
			Creation = mfrRole.Creation;
			LastUpdate = mfrRole.LastUpdate;
		}
	}
}