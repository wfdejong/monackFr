using System;
using System.Collections.Generic;

namespace MonackFr.Mvc.Areas.UserManagement.ViewModels
{
    /// <summary>
    /// View model of user
    /// </summary>
	public class User
	{
        /// <summary>
        /// Id
        /// </summary>
		public int Id { get; set; }

        /// <summary>
        /// Username
        /// </summary>
		public string UserName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
		public string Email { get; set; }

        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Last loging of user
        /// </summary>
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Last login of user, formatted to short date
        /// </summary>
        public string LastLoginFormatted { get { return LastLogin?.ToShortDateString(); } }

        /// <summary>
        /// All groups with selected=true if user is member of that group
        /// </summary>
	    public IEnumerable<Group> Groups { get; set; }
	}
}