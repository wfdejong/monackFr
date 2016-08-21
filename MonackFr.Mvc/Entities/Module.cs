using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MonackFr.Mvc.Entities
{
    public class Module
    {
		/// <summary>
		/// Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The module name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Description of the module
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The author of the module
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		/// The system name used internally. Should be unique in the whole application
		/// </summary>
		public string SystemName { get; set; }
        				
		/// <summary>
		/// Roles available in the module
		/// </summary>		
		public ICollection<Role> Roles { get; set; }

		/// <summary>
		/// The Package to which the module belongs 
		/// </summary>
		[Required]
		public Package Package { get; set; }
    }
}