using System;

namespace MonackFr.Mvc.Areas.UserManagement.ViewModels
{
	public class User
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
        public DateTime? LastLogin { get; set; }
        public string LastLoginFormatted { get { return LastLogin?.ToShortDateString(); } }
    }
}