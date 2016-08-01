using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Areas.UserManagement.ViewModels
{
	public class User
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
        public DateTime? LastLogin { get; set; }
        public string LastLoginFormatted { get { return LastLogin?.ToShortDateString(); } }
    }
}