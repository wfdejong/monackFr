using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.ViewModels
{
	public class MenuItem
	{
		public string Label { get; set; }
		public string Function { get; set; }
		public IEnumerable<MenuItem> MenuItems { get; set; }
	}
}