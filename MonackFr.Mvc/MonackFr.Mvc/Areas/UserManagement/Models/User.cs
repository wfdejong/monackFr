using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using System.ComponentModel.DataAnnotations.Schema;
using MonackFr.Security;

namespace MonackFr.Mvc.Areas.UserManagement.Models
{
	public class User : MfrUser
	{
		#region properties

		[InverseProperty("Users")]
		public virtual ICollection<Role> Roles { get; set; }

		[InverseProperty("Users")]
		public virtual ICollection<Group> Groups { get; set; }

		#endregion //properties

		#region constructors

		public User()
		{
			LastUpdate = DateTime.Now;
			Creation = DateTime.Now;
			LastPaswordChange = DateTime.Now;
		}

		#endregion //constuctors

		#region public functions

		public MembershipUser GetMembershipuser()
		{
			DateTime lastLogin = LastLogin ?? DateTime.MinValue;
			DateTime lastActivity = LastActivity ?? DateTime.MinValue;
			return new MembershipUser(Membership.Provider.Name, UserName, null, Email, "", "", true, true, Creation, lastLogin, lastActivity, LastPaswordChange, DateTime.MinValue);			
		}

		#endregion //public functions
	}
}