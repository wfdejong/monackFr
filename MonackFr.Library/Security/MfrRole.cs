﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MonackFr.Security
{
	public class MfrRole : IMfrRole
	{
		/// <summary>
		/// Id
		/// </summary>
		[Key]		
		public Int32 Id { get; set; }
		
		/// <summary>
		/// Role name
		/// </summary>
		[Required]
		public String Name { get; set; }

		/// <summary>
		/// Role description
		/// </summary>
		public String Description { get; set; }

		/// <summary>
		/// Creation date
		/// </summary>
		public DateTime Creation { get; set; }

		/// <summary>
		/// Last update date
		/// </summary>
		public DateTime LastUpdate { get; set; }

        public MfrRole()
        {
            Creation = DateTime.Now;
            LastUpdate = DateTime.Now;
        }
	}
}
