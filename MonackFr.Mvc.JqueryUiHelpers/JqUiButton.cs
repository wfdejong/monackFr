using MonackFr.Mvc.JqueryUiHelpers.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MonackFr.Mvc.JqueryUiHelpers
{
	/// <summary>
	/// Shows JqueryUi button
	/// </summary>
	public class JqUiButton : JqUi
	{
		private string _label;
		private string _clickEvent;

		internal JqUiButton(HtmlHelper helper, string name)
			: base(helper, name)
		{ }

		/// <summary>
		/// Renders the output
		/// </summary>
		/// <returns>The client side code that renders in the browser</returns>
		public override MvcHtmlString Show()
		{
			StringBuilder button = new StringBuilder();

			button.AppendFormat("<input type=\"button\" id=\"{0}\" value=\"{1}\">", _name, _label);
			button.AppendLine("<script type=\"text/javascript\">");
			button.AppendFormat("var {0} = function () {{", _name);			
			button.Append("$(\"#");
			button.AppendFormat("{0}\")", _name);
			button.Append(".button()");
			button.Append(";");
			button.AppendLine("};");
			button.AppendLine();
			button.Append("$(\"#");
			button.AppendFormat("{0}\")", _name);
			button.AppendFormat(".on(\"click\", {0})", _clickEvent);

			return new MvcHtmlString(button.ToString());
		}

		public JqUiButton Label(string label)
		{
			_label = label;
			return this;
		}

		public JqUiButton OnClick(string clickEvent)
		{
			_clickEvent = clickEvent;
			return this;
		}
	}
}
