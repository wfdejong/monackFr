using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MonackFr.Security;
using MonackFr.Mvc.Entities;

namespace MonackFr.Mvc.Areas.UserManagement.ViewModels
{
	public class CreateUser
	{		
		[Required]
		[MinLength(8)]
		public String UserName { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		public String Email { get; set; }
		
		[Required]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public String Password { get; set; }

		[Required]
		[Compare("Password")]
		[DataType(DataType.Password)]
		public String RetypePassword { get; set; }

		public User Map()
		{
			User user = new User();
			
			user.UserName = UserName;
			user.Password = Password;
			user.Email = Email;
			
			return user;
		}
	}
}