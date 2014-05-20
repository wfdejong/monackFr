using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using MonackFr.Mvc.Entities;

namespace MonackFr.Mvc.Areas.UserManagement.ViewModels
{
	[NotMapped]
	public class CheckBoxListGroup : Group
	{
		public Boolean Checked { get; set; }
	}
}