using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonackFr.Mvc.JqueryUiHelpers.Classes
{
	internal class Column
	{
		internal string Title {get; private set;}
		
		internal string DataField { get; private set; }

		internal bool Visible { get; private set; }

		internal Column(string title, string dataField, bool visible)
		{
			Title = title;
			DataField = dataField;
			Visible = visible;
		}
	}
}
