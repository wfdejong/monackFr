using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MonackFr.Mvc.JqueryUiHelpers
{
	public static class JqueryUiHelper
	{
		public static DataTable DataTable(this HtmlHelper helper)
		{			
			return new DataTable(helper);
		}
	}
}
