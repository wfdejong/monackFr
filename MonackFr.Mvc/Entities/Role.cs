using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MonackFr.Security;

namespace MonackFr.Mvc.Entities
{
	public class Role
	{
        /// <summary>
		/// Id
		/// </summary>
		public int Id { get; set; }
		
		/// <summary>
		/// Role name
		/// </summary>
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// Role description
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Creation date
		/// </summary>
		public DateTime Creation { get; set; }

		/// <summary>
		/// Last update date
		/// </summary>
		public DateTime LastUpdate { get; set; }
		
		public virtual ICollection<User> Users { get; set; }

		public virtual ICollection<Group> Groups { get; set; }
		
		public Role() 
		{
			Creation = DateTime.Now;
			LastUpdate = DateTime.Now;
		}		
	}
}