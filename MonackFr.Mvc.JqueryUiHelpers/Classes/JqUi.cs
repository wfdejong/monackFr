using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MonackFr.Mvc.JqueryUiHelpers.Classes
{
	public abstract class JqUi
	{
		protected HtmlHelper _helper;
		protected string _name;
		
		internal JqUi(HtmlHelper helper, string name)
		{
			_helper = helper;
			_name = name;
		}

		/// <summary>
		/// Renders the output
		/// </summary>
		/// <returns>The client side code that renders in the browser</returns>
		public abstract MvcHtmlString Show();
	}
}
