using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace MonackFr.Mvc.Entities
{
	public class User
	{
		/// <summary>
		/// User Id
		/// </summary>
	    public int Id { get; set; }

		/// <summary>
		/// Username
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Password
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Email
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Creation date
		/// </summary>		
		[Column(TypeName = "DateTime2")]
		public DateTime Creation { get; set; }

		/// <summary>
		/// Last update date
		/// </summary>
		[Column(TypeName = "DateTime2")]
		public DateTime LastUpdate { get; set; }

		/// <summary>
		/// Last login date
		/// </summary>
		[Column(TypeName = "DateTime2")]
		public DateTime? LastLogin { get; set; }

		/// <summary>
		/// Last activity date
		/// </summary>
		[Column(TypeName = "DateTime2")]
		public DateTime? LastActivity { get; set; }

		/// <summary>
		/// last password change date
		/// </summary>
		[Column(TypeName = "DateTime2")]
		public DateTime LastPaswordChange { get; set; }
		
		public virtual ICollection<Role> Roles { get; set; }

		public virtual ICollection<Group> Groups { get; set; }
        		
		public User()
		{
			LastUpdate = DateTime.Now;
			Creation = DateTime.Now;
			LastPaswordChange = DateTime.Now;
		}
	}
}