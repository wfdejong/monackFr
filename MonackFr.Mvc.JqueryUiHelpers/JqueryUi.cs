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
		public static JqUiButton UiButton(this HtmlHelper helper, string name)
		{
			return new JqUiButton(helper, name);
		}

		/// <summary>
		/// Returns a new datatable class
		/// </summary>
		/// <param name="helper"></param>
		/// <returns></returns>
		public static DataTable DataTable(this HtmlHelper helper, string name)
		{			
			return new DataTable(helper, name);
		}
	}
}
