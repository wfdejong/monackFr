using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Entities
{
    public class Module
    {
		/// <summary>
		/// Id
		/// </summary>
		public int Id { get; set; }
		
		/// <summary>
		/// Roles available in the module
		/// </summary>
		public ICollection<Role> Roles { get; set; }
    }
}