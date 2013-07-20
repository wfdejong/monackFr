using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonackFr.Mvc.Areas.UserManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonackFr.Mvc.Areas.UserManagement.ViewModels
{
	[NotMapped]
	public class CheckBoxListRole : Role
	{		
		public Boolean Checked { get; set; }		
	}
}