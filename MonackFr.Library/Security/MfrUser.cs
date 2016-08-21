using System;

namespace MonackFr.Security
{
	public class MfrUser
	{
		/// <summary>
		/// User Id
		/// </summary>
		public Int32 Id { get; set; }

		/// <summary>
		/// Username
		/// </summary>
		public String UserName { get; set; }

		/// <summary>
		/// Password
		/// </summary>
		public String Password { get; set; }

		/// <summary>
		/// Email
		/// </summary>
		public String Email { get; set; }

		/// <summary>
		/// Creation date
		/// </summary>
		public DateTime Creation { get; set; }

		/// <summary>
		/// Last update date
		/// </summary>
		public DateTime LastUpdate { get; set; }

		/// <summary>
		/// Last login date
		/// </summary>
		public DateTime? LastLogin { get; set; }

		/// <summary>
		/// Last activity date
		/// </summary>
		public DateTime? LastActivity { get; set; }

		/// <summary>
		/// last password change date
		/// </summary>
		public DateTime LastPaswordChange { get; set; }
	}
}
